#ifndef CLICKABLEELLIPSEITEM_H
#define CLICKABLEELLIPSEITEM_H

#include <QGraphicsEllipseItem>
#include <QGraphicsSceneMouseEvent>
#include <QObject>

class ClickableEllipseItem : public QObject, public QGraphicsEllipseItem
{
    Q_OBJECT  

public:
    ClickableEllipseItem(int placeId, QGraphicsItem* parent = nullptr)
        : QGraphicsEllipseItem(parent), placeId(placeId) {
        setFlag(QGraphicsItem::ItemIsSelectable);  // 允许选择
        setFlag(QGraphicsItem::ItemIsMovable);     // 允许移动
    }

    int getPlaceId() const {
        return placeId;
    }

protected:
    void mousePressEvent(QGraphicsSceneMouseEvent* event) override {
        emit clicked(placeId);  // 发出点击事件信号
        QGraphicsEllipseItem::mousePressEvent(event);  // 保持原有事件
    }

signals:
    void clicked(int placeId);  // 自定义点击事件信号

private:
    int placeId;  // 对应的建筑物ID
};

#endif // CLICKABLEELLIPSEITEM_H
