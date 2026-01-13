#ifndef POINTITEM_H
#define POINTITEM_H

#include <QGraphicsEllipseItem>
#include <QGraphicsSceneMouseEvent>
#include "Point.h"  // Point类的头文件

class PointItem : public QGraphicsEllipseItem {
public:
    PointItem(const Point& point, QGraphicsItem* parent = nullptr);

    // 重写 paint 方法进行自定义绘制
    void paint(QPainter* painter, const QStyleOptionGraphicsItem* option, QWidget* widget) override;

    // 返回点的ID
    int getId() const { return point.id; }

private:
    Point point;  // 用于存储点的具体信息
};

#endif // POINTITEM_H
