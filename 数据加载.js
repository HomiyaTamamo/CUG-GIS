// 这里面写了那三类数据的载入方案
let gltfEntity = null;
let tileset = null;
let geojsonDataSource = null;

async function loadModel() {
  const inputElement = document.getElementById('modelFileInput');
  const file = inputElement.files[0];
  if (!file) {
    console.error('未选择文件');
    alert('请先选择一个模型文件');
    return;
  }
}
async function loadModel() {
  const inputElement = document.getElementById('modelFileInput');
  const file = inputElement.files[0];
  if (!file) {
    console.error('未选择文件');
    return;
  }

  const fileExtension = file.name.split('.').pop().toLowerCase();

  try {
    // 清理现有模型
    if (gltfEntity) {
      viewer.entities.remove(gltfEntity);
      gltfEntity = null;
    }
    if (tileset) {
      viewer.scene.primitives.remove(tileset);
      tileset = null;
    }
    if (geojsonDataSource) {
      viewer.dataSources.remove(geojsonDataSource);
      geojsonDataSource = null;
    }

    if (fileExtension === 'gltf' || fileExtension === 'glb') {
      // 加载 glTF / glb 模型
      const modelUrl = URL.createObjectURL(file);
      gltfEntity = viewer.entities.add({
        name: file.name,
        position: Cesium.Cartesian3.fromDegrees(116.3974, 39.9093, 100), // 默认位置：北京上空
        model: {
          uri: modelUrl,
          scale: 10.0,
          minimumPixelSize: 64,
          maximumScale: 200
        }
      });
      await viewer.flyTo(gltfEntity);

    } else if (fileExtension === '3dtiles') {
      // 兼容旧方式（通过 .3dtiles 文件加载）
      const tilesetUrl = URL.createObjectURL(file);
      tileset = new Cesium.Cesium3DTileset({
        url: tilesetUrl
      });
      viewer.scene.primitives.add(tileset);
      await viewer.zoomTo(tileset, new Cesium.HeadingPitchRange(0, -0.5, 1000));

    } else if (fileExtension === 'json') {
      // 尝试判断是 3D Tiles 的 tileset.json 文件
      const reader = new FileReader();
      reader.onload = function (event) {
        try {
          const jsonText = event.target.result;
          const jsonObj = JSON.parse(jsonText);

          // 简单判断是否为 3D Tiles 的 tileset 文件（存在 asset 和 geometricError 字段）
          if (jsonObj.asset && jsonObj.geometricError) {
            const blob = new Blob([jsonText], { type: 'application/json' });
            const url = URL.createObjectURL(blob);
            tileset = new Cesium.Cesium3DTileset({
              url: url
            });
            viewer.scene.primitives.add(tileset);
            viewer.zoomTo(tileset, new Cesium.HeadingPitchRange(0, -0.5, 1000));
          } else {
            alert("JSON 文件不是有效的 tileset.json 格式");
          }
        } catch (e) {
          console.error('JSON 解析失败:', e);
          alert("JSON 文件解析失败，请检查格式");
        }
      };
      reader.readAsText(file);

    } else if (fileExtension === 'geojson') {
    // 加载 GeoJSON 文件
    const reader = new FileReader();
    reader.onload = function (event) {
        try {
            const geoJsonText = event.target.result;
            const geoJsonObj = JSON.parse(geoJsonText);

            Cesium.GeoJsonDataSource.load(geoJsonObj).then(function (ds) {
                geojsonDataSource = ds;
                viewer.dataSources.add(ds);
                viewer.flyTo(ds);

                // 为每个要素应用样式
                ds.entities.values.forEach(function(entity) {
                    // 检查是否具有 height 属性
                    if (entity.properties && entity.properties.height) {
                        const height = entity.properties.height.getValue();

                        // 设置点的高度
                        if (entity.position) {
                            const cartographic = Cesium.Cartographic.fromCartesian(entity.position.getValue(Cesium.JulianDate.now()));
                            const longitude = Cesium.Math.toDegrees(cartographic.longitude);
                            const latitude = Cesium.Math.toDegrees(cartographic.latitude);
                            entity.position = Cesium.Cartesian3.fromDegrees(longitude, latitude, height);
                        }

                        // 设置点的颜色，根据高度分色
                        entity.point = new Cesium.PointGraphics({
                            color: height >= 100 ? Cesium.Color.RED :
                                   height >= 50 ? Cesium.Color.YELLOW :
                                   Cesium.Color.GREEN,
                            pixelSize: 10
                        });
                    }

                    // 添加注记 (这里假设有 name 属性作为注记)
                    if (entity.properties && entity.properties.name) {
                        const name = entity.properties.name.getValue();
                        entity.label = new Cesium.LabelGraphics({
                            text: name,
                            font: '16px sans-serif',
                            fillColor: Cesium.Color.WHITE,
                            style: Cesium.LabelStyle.FILL_AND_OUTLINE,
                            outlineWidth: 2,
                            verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
                            pixelOffset: new Cesium.Cartesian2(0, -10)
                        });
                    }
                });

            }).catch(function (error) {
                console.error('GeoJSON 加载失败:', error);
                alert("GeoJSON 加载失败，请检查数据格式。");
            });
        } catch (e) {
            console.error('GeoJSON 文件解析错误:', e);
            alert("GeoJSON 文件解析错误，请检查是否是合法的 JSON 格式");
        }
    };
    reader.readAsText(file);
}
 else {
      alert('不支持的文件类型：' + fileExtension);
    }

  } catch (error) {
    console.error('文件加载失败:', error);
    alert('文件加载失败，请检查文件内容和格式');
  }
}
Cesium.Ion.defaultAccessToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJkNWNmMzViZi1hNjA4LTQ3MmMtOTQ1ZS1lOGM4YzkzMDZjMDYiLCJpZCI6MzA2NzI2LCJpYXQiOjE3NDgzNDgxNDB9.nZmuQzezuTQV15U1fHJyMRVMn1_ZtVSOmATa7sbQDyM';

async function loadCesium3DTiles() {
    try {
        const resource = await Cesium.IonResource.fromAssetId(96188);
        const tileset = await Cesium.Cesium3DTileset.fromUrl(resource, {
            show: true,
            maximumScreenSpaceError: 2,
            dynamicScreenSpaceError: true
        });

        viewer.scene.primitives.add(tileset);

        // 等待 Tileset 完全加载
        await tileset.readyPromise;

        // 进一步拉近视角
        const boundingSphere = tileset.boundingSphere;
        const offset = new Cesium.HeadingPitchRange(0.0, -0.3, boundingSphere.radius * 0.8);

        // 平滑飞行到模型
        viewer.flyTo(tileset, {
            duration: 2.0,
            offset: offset
        });
    } catch (error) {
        console.error("加载 3D Tiles 失败：", error);
        alert("3D Tiles 加载失败，请检查 token 权限或网络连接。");
    }
}




