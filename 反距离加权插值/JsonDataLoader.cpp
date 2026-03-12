// JsonDataLoader.cpp
#include "JsonDataLoader.h"
#include <QFile>
#include <QJsonDocument>
#include <QJsonObject>
#include <QJsonArray>
#include <QGraphicsScene>
#include <QGraphicsLineItem>
#include <QPen>
#include <QBrush>
#include <QDebug>

JsonDataLoader::JsonDataLoader() {
}

bool JsonDataLoader::loadJsonData(const QString& filename, QGraphicsScene* scene) {
    QFile file(filename);
    if (!file.open(QIODevice::ReadOnly)) {
        qWarning() << "Unable to open file:" << filename;
        return false;
    }

    // 获取坐标系信息
    QJsonDocument jsonDoc = QJsonDocument::fromJson(file.readAll());
    QJsonObject rootObj = jsonDoc.object();
    if (rootObj.contains("crs")) {
        QJsonObject crsObject = rootObj["crs"].toObject();
        if (crsObject.contains("properties")) {
            QJsonObject properties = crsObject["properties"].toObject();
            QString crsName = properties["name"].toString();

            // 只处理 CRS84
            if (crsName == "urn:ogc:def:crs:OGC:1.3:CRS84") {
                qDebug() << "Using CRS84: WGS84 (Longitude, Latitude)";
                // 这里可以进行经纬度到屏幕坐标的转换
            }
        }
    }

    if (!rootObj.contains("features")) {
        qWarning() << "GeoJSON does not contain 'features' array.";
        return false;
    }

    QJsonArray featuresArray = rootObj["features"].toArray();
    int totalFeatures = 0;

    //实际为GeoJSON数据
    for (const QJsonValue& featureVal : featuresArray) {
        QJsonObject featureObj = featureVal.toObject();
        QJsonObject propertiesObj = featureObj["properties"].toObject();
        QJsonObject geometryObj = featureObj["geometry"].toObject();

        if (geometryObj["type"].toString() != "LineString") {
            continue;  //处理 LineString 
        }

        QJsonArray coordinates = geometryObj["coordinates"].toArray();
        double elev = propertiesObj["ELEV"].toDouble();  

        QVector<QPointF> contourPoints;  
        for (const QJsonValue& coordVal : coordinates) {
            QJsonArray coord = coordVal.toArray();
            if (coord.size() >= 2) {
                double lon = coord[0].toDouble();  // Longitude
                double lat = coord[1].toDouble();  // Latitude

                // 经纬度转换为屏幕坐标，简单缩放（可以调整缩放因子）
                double x = lon * 40000;  // 简单的线性缩放
                double y = lat * 40000;  // 简单的线性缩放

                contourPoints.append(QPointF(x, y));  
            }
        }

        contours.append(contourPoints);  
        elevations.append(elev);         

        totalFeatures++;
    }

    qDebug() << "Loaded" << totalFeatures << "features from GeoJSON";

    // 可视化轮廓
    for (int i = 0; i < contours.size(); ++i) {
        const QVector<QPointF>& contour = contours[i];
        QGraphicsPolygonItem* polygonItem = new QGraphicsPolygonItem(QPolygonF(contour));
        polygonItem->setPen(QPen(Qt::blue));  
        polygonItem->setBrush(QBrush(Qt::black, Qt::Dense3Pattern));  
        scene->addItem(polygonItem);
    }

    return true;
}


// 获取每条等高线的坐标点
QVector<QVector<QPointF>> JsonDataLoader::getContours() const {
    return contours;
}

// 获取每条等高线的高程值
QVector<double> JsonDataLoader::getElevations() const {
    return elevations;
}
