let visibilitySelectMode = false;
let visibilityHandler = null;
let observerPosition = null;
let targetPosition = null;

function toggleVisibilitySelectMode() {
    visibilitySelectMode = !visibilitySelectMode;
    const btn = document.getElementById('toggleSelectMode');
    if (visibilitySelectMode) {
        btn.innerText = '禁用通视选取模式';
        activateVisibilitySelectMode();
    } else {
        btn.innerText = '启用通视选取模式';
        deactivateVisibilitySelectMode();
    }
}

function activateVisibilitySelectMode() {
    viewer.scene.canvas.style.cursor = 'crosshair';
    visibilityHandler = new Cesium.ScreenSpaceEventHandler(viewer.scene.canvas);
    visibilityHandler.setInputAction((click) => {
        const cartesian = viewer.scene.pickPosition(click.position);
        if (!cartesian) {
            alert('未拾取到位置，请再试一次');
            return;
        }

        if (!observerPosition) {
            observerPosition = cartesian;
            viewer.entities.add({
                position: observerPosition,
                point: {
                    pixelSize: 10,
                    color: Cesium.Color.BLUE
                },
                label: {
                    text: '观察点',
                    font: '14pt sans-serif',
                    fillColor: Cesium.Color.WHITE,
                    style: Cesium.LabelStyle.FILL_AND_OUTLINE,
                    outlineWidth: 2,
                    verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
                    pixelOffset: new Cesium.Cartesian2(0, -10)
                }
            });
        } else if (!targetPosition) {
            targetPosition = cartesian;
            viewer.entities.add({
                position: targetPosition,
                point: {
                    pixelSize: 10,
                    color: Cesium.Color.RED
                },
                label: {
                    text: '目标点',
                    font: '14pt sans-serif',
                    fillColor: Cesium.Color.WHITE,
                    style: Cesium.LabelStyle.FILL_AND_OUTLINE,
                    outlineWidth: 2,
                    verticalOrigin: Cesium.VerticalOrigin.BOTTOM,
                    pixelOffset: new Cesium.Cartesian2(0, -10)
                }
            });
            performVisibilityAnalysis(observerPosition, targetPosition);
            observerPosition = null;
            targetPosition = null;
        }
    }, Cesium.ScreenSpaceEventType.LEFT_CLICK);
}

function deactivateVisibilitySelectMode() {
    viewer.scene.canvas.style.cursor = 'default';
    if (visibilityHandler) {
        visibilityHandler.destroy();
        visibilityHandler = null;
    }
    observerPosition = null;
    targetPosition = null;
}

function performVisibilityAnalysis(observer, target) {
    const ray = new Cesium.Ray(observer, Cesium.Cartesian3.normalize(Cesium.Cartesian3.subtract(target, observer, new Cesium.Cartesian3()), new Cesium.Cartesian3()));
    const result = viewer.scene.pickFromRay(ray, []);

    let isVisible = true;
    if (result && result.position) {
        const distanceToObstacle = Cesium.Cartesian3.distance(observer, result.position);
        const distanceToTarget = Cesium.Cartesian3.distance(observer, target);
        if (distanceToObstacle < distanceToTarget) {
            isVisible = false;
        }
    }

    const color = isVisible ? Cesium.Color.GREEN : Cesium.Color.RED;
    viewer.entities.add({
        polyline: {
            positions: [observer, target],
            width: 3,
            material: color
        }
    });

    alert(`通视分析完成：视线${isVisible ? '未被阻挡' : '被阻挡'}`);
}
