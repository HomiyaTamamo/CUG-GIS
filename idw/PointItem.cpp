#include "PointItem.h"
#include <QPainter>

PointItem::PointItem(const Point& point, QGraphicsItem* parent)
    : QGraphicsEllipseItem(parent), point(point) {
    setRect(-5, -5, 10, 10);  // 设置点的大小
    setBrush(Qt::blue);  // 设置点的颜色
}

void PointItem::paint(QPainter* painter, const QStyleOptionGraphicsItem* option, QWidget* widget) {
    // 绘制一个圆形点
    painter->setBrush(brush());
    painter->drawEllipse(rect());
}
