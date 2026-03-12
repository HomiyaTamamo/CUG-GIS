#ifndef MAPVIEW_H
#define MAPVIEW_H

#include <QGraphicsView>
#include <QGraphicsScene>
#include <QPainter>
#include "Point.h"

class MapView : public QGraphicsView {
    Q_OBJECT
public:
    explicit MapView(QWidget* parent = nullptr);
    void addPointsToScene();  // 将点添加到场景中
    void drawPointsAndNames(QPainter& painter);  // 绘制点和名称

private:
    QGraphicsScene* scene;   // 存储场景
    std::vector<Point> points;  // 存储点的数据
};

#endif // MAPVIEW_H
