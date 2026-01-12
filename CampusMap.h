#ifndef CAMPUSMAP_H
#define CAMPUSMAP_H

#include <QVector>
#include <QString>
#include <QPointF>
#include <QMap>
#include <QJsonDocument>
#include <QJsonObject>
#include <QJsonArray>
#include <QFile>

// 场所结构体
struct Place {
    int id;
    QString name;
    QString category;
    QPointF coordinates;
};

// 校园图类
class CampusMap {
public:
    QVector<Place> places;                        // 存储所有场所
    QMap<int, QMap<int, double>> adjMatrix;       // 邻接表存储图，int->int->权重
    QMap<int, int> idToIndex;                    // ID 到矩阵索引的映射
    QMap<int, int> indexToId;                    // 索引到 ID 的反向映射

    // 加载和保存地图数据
    void loadMapFromJson(const QString& filePath);
    void saveMapToJson(const QString& filePath);

    // 最短路径求解
    QVector<int> shortestPath(int startId, int endId);

private:
    double calculateDistance(const QPointF& a, const QPointF& b);
    void initializeGraph(const QVector<Place>& places, const QVector<QMap<int, double>>& edges);
    bool lineIntersect(const QPointF& A1, const QPointF& A2, const QPointF& B1, const QPointF& B2, QPointF& intersection);
    QVector<QPair<int, int>> splitRoadAtIntersection(int from, int to, const QPointF& intersection);
    void updateAdjMatrix(int from, int to, double weight);
};

#endif // CAMPUSMAP_H
