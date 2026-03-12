#ifndef POINT_H
#define POINT_H

#include <QString>
#include <QPainter>

class Point {
public:
    Point(int id, const QString& name, float locationX, float locationY)
        : id(id), name(name), locationX(locationX), locationY(locationY) {}

    int getObjectId() const { return id; }
    const QString& getName() const { return name; }
    float getLocationX() const { return locationX; }
    float getLocationY() const { return locationY; }

    // 绘制点
    void draw(QPainter& painter, const QPointF& center) const;

    // 绘制名称
    void drawName(QPainter& painter, const QPointF& center) const;

    // 获取显示的名称（简化版）
    QString getDisplayName() const;
    int id;            // 点的ID
    QString name;      // 点的名称
    float locationX;   // 经度
    float locationY;   // 纬度

private:
    
};

#endif // POINT_H
