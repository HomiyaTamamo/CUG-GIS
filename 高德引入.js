// 四个图层以及地图立体化的相关代码
Cesium.Ion.defaultAccessToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJkNWNmMzViZi1hNjA4LTQ3MmMtOTQ1ZS1lOGM4YzkzMDZjMDYiLCJpZCI6MzA2NzI2LCJpYXQiOjE3NDgzNDgxNDB9.nZmuQzezuTQV15U1fHJyMRVMn1_ZtVSOmATa7sbQDyM';
// 初始化图层索引
let currentImagery = 0;
// 定义图层列表和名称对应关系
const imageryProviders = [
  {
    name: "高德矢量",
    provider: new Cesium.UrlTemplateImageryProvider({
      url: 'http://webrd01.is.autonavi.com/appmaptile?&scale=1&lang=zh_cn&style=8&x={x}&y={y}&z={z}',
      subdomains: ['1', '2', '3', '4'],
      maximumLevel: 18
    }),
  },
  {
    name: "高德影像",
    provider: new Cesium.UrlTemplateImageryProvider({
      url: 'http://webst01.is.autonavi.com/appmaptile?style=6&x={x}&y={y}&z={z}',
      subdomains: ['1', '2', '3', '4'],
      maximumLevel: 18
    }),
  },
  {
    name: "高德路网",
    provider: new Cesium.UrlTemplateImageryProvider({
      url: 'http://webst01.is.autonavi.com/appmaptile?style=8&x={x}&y={y}&z={z}',
      subdomains: ['1', '2', '3', '4'],
      maximumLevel: 18
    }),
  },
  {
    name: "Cesium地形",
    provider: new Cesium.UrlTemplateImageryProvider({
      url: "https://{s}.basemaps.cartocdn.com/light_all/{z}/{x}/{y}.png",
      subdomains: ["a", "b", "c", "d"],
      credit: "CartoDB"
    }),
  }
];

// 切换图层函数
function switchImagery() {
  // 移除所有影像图层
  viewer.imageryLayers.removeAll();

  // 获取当前图层配置
  const selected = imageryProviders[currentImagery];

  // 添加选中的影像图层
  viewer.imageryLayers.addImageryProvider(selected.provider);

  // 更新按钮文字
  document.querySelector('#toolbar button').innerText =
    `切换影像 (当前: ${selected.name})`;

  // 循环索引
  currentImagery = (currentImagery + 1) % imageryProviders.length;
}

let terrainEnabled = false;  // 初始为禁用地形

async function switchTerrain() {
  const button = document.getElementById("terrainBtn");

  if (terrainEnabled) {
    // 禁用地形（恢复为椭球体）
    viewer.terrainProvider = new Cesium.EllipsoidTerrainProvider();
    viewer.scene.globe.terrainExaggeration = 1.0;  // 恢复正常比例
    button.innerText = "禁用地形";
  } else {
    // 启用三维地形
    viewer.terrainProvider = await Cesium.CesiumTerrainProvider.fromIonAssetId(1);
    viewer.scene.globe.depthTestAgainstTerrain = true;
    viewer.scene.globe.enableLighting = true;

    // 设置地形夸张倍率
    viewer.scene.globe.terrainExaggeration = 5.0;  
    button.innerText = "启用地形";
  }

  terrainEnabled = !terrainEnabled;
}


