#ifndef JSONDATALOADER_H
#define JSONDATALOADER_H

#include <QVector>
#include <QPointF>
#include <QGraphicsScene>  // 添加这一行
#include <QString>
//这几个库没用上
//#include <QgsVectorLayer.h>
//#include <QgsVectorFileWriter.h>
//#include <QgsProject.h>

class JsonDataLoader {
public:
    JsonDataLoader();
    bool loadJsonData(const QString& filename, QGraphicsScene* scene);


    QVector<QPointF> getPoints() const;
    QVector<double> getValues() const;
    // 获取加载的点数据（每条等高线的坐标点）
    QVector<QVector<QPointF>> getContours() const;

    // 获取每条等高线的高程值
    QVector<double> getElevations() const;


private:
    QVector<QPointF> points;
    QVector<double> values;
    QVector<QVector<QPointF>> contours;  // 存储每条等高线的坐标点
    QVector<double> elevations;          // 存储对应的高程值
};

#endif // JSONDATALOADER_H
