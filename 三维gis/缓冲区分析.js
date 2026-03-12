// 打开缓冲区输入弹窗
function openBufferInput() {
    document.getElementById('bufferInputModal').style.display = 'block';
}

// 关闭缓冲区输入弹窗
function closeBufferInput() {
    document.getElementById('bufferInputModal').style.display = 'none';
}

// 提交缓冲区参数并执行分析
function submitBuffer() {
    const lon = parseFloat(document.getElementById('lonInput').value);
    const lat = parseFloat(document.getElementById('latInput').value);
    const radius = parseFloat(document.getElementById('radiusInput').value);

    if (isNaN(lon) || isNaN(lat) || isNaN(radius)) {
        alert('请输入有效的经度、纬度和半径。');
        return;
    }

    bufferAnalysis(lon, lat, radius);
    closeBufferInput();
}
function bufferAnalysis(lon, lat, bufferRadius) {
    const position = Cesium.Cartesian3.fromDegrees(lon, lat);
    const radius = bufferRadius; // 缓冲区半径，单位：米

    const bufferCircle = new Cesium.CircleGeometry({
        center: position,
        radius: radius
    });

    const circleInstance = new Cesium.GeometryInstance({
        geometry: bufferCircle,
        attributes: {
            color: Cesium.ColorGeometryInstanceAttribute.fromColor(Cesium.Color.BLUE.withAlpha(0.5))
        }
    });

    const primitive = new Cesium.Primitive({
        geometryInstances: [circleInstance],
        appearance: new Cesium.PerInstanceColorAppearance({
            translucent: true
        }),
        releaseGeometryInstances: false
    });

    viewer.scene.primitives.add(primitive);
}
