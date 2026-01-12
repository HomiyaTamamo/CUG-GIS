#include "MapView.h"
#include <QGraphicsItem>
#include <QGraphicsScene>
#include "PointItem.h"

MapView::MapView(QWidget* parent)
    : QGraphicsView(parent), scene(new QGraphicsScene(this)) {
    setScene(scene);
}

void MapView::addPointsToScene() {
    // 添加点到场景中
    for (const Point& point : points) {
        PointItem* item = new PointItem(point);
        scene->addItem(item);
    }
}

void MapView::drawPointsAndNames(QPainter& painter) {
    for (const Point& point : points) {
        QPointF pointPosition = mapToScene(point.getLocationX(), point.getLocationY());
        point.draw(painter, pointPosition);
        point.drawName(painter, pointPosition);
    }
}
