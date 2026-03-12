Cesium.Ion.defaultAccessToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJkNWNmMzViZi1hNjA4LTQ3MmMtOTQ1ZS1lOGM4YzkzMDZjMDYiLCJpZCI6MzA2NzI2LCJpYXQiOjE3NDgzNDgxNDB9.nZmuQzezuTQV15U1fHJyMRVMn1_ZtVSOmATa7sbQDyM';
    const terrainProviders = {
      None: () => Promise.resolve(new Cesium.EllipsoidTerrainProvider()),
      CesiumWorldTerrain: () => Cesium.CesiumTerrainProvider.fromIonAssetId(1)
    };

    const viewer = new Cesium.Viewer("cesiumContainer", {
      baseLayerPicker: false,
      imageryProvider: new Cesium.IonImageryProvider({ assetId: 2 }),
      terrainProvider: new Cesium.EllipsoidTerrainProvider()
    });

    const imagerySelect = document.getElementById("imagery");
    const terrainSelect = document.getElementById("terrain");
    const viewpointSelect = document.getElementById("viewpoint");
    const fovSelect = document.getElementById("fov");

    let gltfEntity = null;
    let tileset = null;
    let geojsonDataSource = null;

    imagerySelect.addEventListener("change", async () => {
      const provider = await imageryProviders[imagerySelect.value]();
      while (viewer.imageryLayers.length > 0) {
        viewer.imageryLayers.remove(viewer.imageryLayers.get(0));
      }
      viewer.imageryLayers.addImageryProvider(provider, 0);
    });

    terrainSelect.addEventListener("change", async () => {
      viewer.terrainProvider = await terrainProviders[terrainSelect.value]();
      viewer.terrainProviderChanged.raiseEvent();
    });

    fovSelect.addEventListener("change", () => {
      const fovMap = { narrow: 20, normal: 60, wide: 90 };
      const fovDegrees = fovMap[fovSelect.value] || 60;
      viewer.camera.frustum = new Cesium.PerspectiveFrustum({
        fov: Cesium.Math.toRadians(fovDegrees),
        aspectRatio: viewer.camera.frustum.aspectRatio,
        near: viewer.camera.frustum.near,
        far: viewer.camera.frustum.far,
      });
    });

    viewpointSelect.addEventListener("change", () => {
      const views = {
        default: {
          destination: Cesium.Cartesian3.fromDegrees(116.3974, 39.9093, 1000000),
          orientation: { heading: 0, pitch: Cesium.Math.toRadians(-90), roll: 0 }
        },
        beijing: {
          destination: Cesium.Cartesian3.fromDegrees(116.4074, 39.9042, 1500),
          orientation: { heading: 0, pitch: Cesium.Math.toRadians(-30), roll: 0 }
        },
        everest: {
          destination: Cesium.Cartesian3.fromDegrees(86.9250, 27.9881, 3000),
          orientation: { heading: 0, pitch: Cesium.Math.toRadians(-45), roll: 0 }
        },
        grandcanyon: {
          destination: Cesium.Cartesian3.fromDegrees(-112.1129, 36.1069, 3000),
          orientation: { heading: 0, pitch: Cesium.Math.toRadians(-45), roll: 0 }
        },
        free: null
      };
      const view = views[viewpointSelect.value];
      if (view) viewer.camera.flyTo(view);
    });