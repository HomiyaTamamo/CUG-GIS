#include "GeometryItem.h"
#include <QPainter>
#include <QGraphicsSceneWheelEvent>  // 包含 QGraphicsSceneWheelEvent 头文件
#include <cmath>  // 包含用于 pow 函数的头文件

GeometryItem::GeometryItem(OGRGeometry* geom, QGraphicsItem* parent)
    : QGraphicsItem(parent), geometry(geom), currentScaleFactor(1.0)
{
    setAcceptHoverEvents(true);
}

GeometryItem::~GeometryItem()
{
    // 假设 geometry 对象由其他地方管理，不需要手动删除
}

QRectF GeometryItem::boundingRect() const
{
    if (!geometry)
        return QRectF();

    OGREnvelope env;
    geometry->getEnvelope(&env);
    QPointF topLeft = mapToViewCoordinates(env.MinX, env.MaxY);
    QPointF bottomRight = mapToViewCoordinates(env.MaxX, env.MinY);

    QRectF rect(topLeft, bottomRight);
    rect.setWidth(rect.width() * currentScaleFactor);
    rect.setHeight(rect.height() * currentScaleFactor);

    return rect;
}

void GeometryItem::paint(QPainter* painter, const QStyleOptionGraphicsItem* option, QWidget* widget)
{
    Q_UNUSED(option)
        Q_UNUSED(widget)

        if (!geometry)
            return;

    QPen pen(Qt::black, 0.05); // 可以根据需要调整画笔的设置

    switch (geometry->getGeometryType())
    {
    case wkbPoint:
    {
        OGRPoint* point = dynamic_cast<OGRPoint*>(geometry);
        if (point)
        {
            QPointF viewPoint = mapToViewCoordinates(point->getX(), point->getY());
            painter->setPen(pen);
            painter->drawPoint(viewPoint);
        }
        break;
    }
    case wkbLineString:
    {
        OGRLineString* line = dynamic_cast<OGRLineString*>(geometry);
        if (line)
        {
            QPainterPath path;
            painter->setPen(pen); // 设置画笔
            for (int i = 0; i < line->getNumPoints(); ++i) {
                QPointF viewPoint = mapToViewCoordinates(line->getX(i), line->getY(i));
                if (i == 0)
                    path.moveTo(viewPoint);
                else
                    path.lineTo(viewPoint);
            }
            painter->drawPath(path);
        }
        break;
    }
    case wkbPolygon:
    {
        OGRPolygon* polygon = dynamic_cast<OGRPolygon*>(geometry);
        if (polygon)
        {
            QPainterPath path;
            painter->setPen(pen); // 设置画笔
            OGRLinearRing* ring = polygon->getExteriorRing();
            for (int i = 0; i < ring->getNumPoints(); ++i) {
                QPointF viewPoint = mapToViewCoordinates(ring->getX(i), ring->getY(i));
                if (i == 0)
                    path.moveTo(viewPoint);
                else
                    path.lineTo(viewPoint);
            }
            painter->drawPath(path);
        }
        break;
    }
    default:
        break;
    }
}


void GeometryItem::wheelEvent(QGraphicsSceneWheelEvent* event)
{
    qreal scaleFactor = std::pow(2.0, event->delta() / 240.0); // 根据鼠标滚轮变化计算缩放因子
    currentScaleFactor *= scaleFactor;
    prepareGeometryChange();
    update();
}

void GeometryItem::mouseMoveEvent(QGraphicsSceneMouseEvent* event)
{
    // 处理鼠标移动事件的代码
}

QPointF GeometryItem::mapToViewCoordinates(double x, double y) const
{
    qreal scaleFactor = currentScaleFactor * 100.0;  // 根据需要调整缩放因子
    qreal offsetX = 0.0;        // 根据需要调整 X 偏移量
    qreal offsetY = 0.0;        // 根据需要调整 Y 偏移量

    // 将地理坐标转换为视图坐标
    QPointF viewPoint;
    viewPoint.setX(x * scaleFactor + offsetX);
    viewPoint.setY(-y * scaleFactor + offsetY);  // Y 轴取负因为视图 Y 轴向下增加
    return viewPoint;
}
