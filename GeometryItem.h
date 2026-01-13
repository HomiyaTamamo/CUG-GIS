#ifndef GEOMETRYITEM_H
#define GEOMETRYITEM_H

#include <QGraphicsItem>
#include "ogr_geometry.h"

class GeometryItem : public QGraphicsItem {
public:
    GeometryItem(OGRGeometry* geom, QGraphicsItem* parent = nullptr);
    ~GeometryItem();

    QRectF boundingRect() const override;
    void paint(QPainter* painter, const QStyleOptionGraphicsItem* option, QWidget* widget = nullptr) override;

protected:
    void wheelEvent(QGraphicsSceneWheelEvent* event) override;
    void mouseMoveEvent(QGraphicsSceneMouseEvent* event) override;
    QPointF mapToViewCoordinates(double x, double y) const;

private:
    OGRGeometry* geometry;
    qreal currentScaleFactor;
};

#endif // GEOMETRYITEM_H
