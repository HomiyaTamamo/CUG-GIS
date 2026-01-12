#include "CampusMap.h"
#include <QJsonValue>
#include <limits>
#include <queue>
#include <cmath>
#include <QSet>


// 初始化地图数据
void CampusMap::loadMapFromJson(const QString& filePath) {
    QFile file(filePath);
    if (!file.open(QIODevice::ReadOnly | QIODevice::Text)) {
        return;
    }

    QByteArray data = file.readAll();
    QJsonDocument doc = QJsonDocument::fromJson(data);
    QJsonObject root = doc.object();

    // 清空旧数据
    places.clear();
    adjMatrix.clear();

    // 加载场所信息
    QJsonArray placesArray = root["places"].toArray();
    for (const QJsonValue& value : placesArray) {
        QJsonObject obj = value.toObject();
        Place place;
        place.id = obj["id"].toInt();
        place.name = obj["name"].toString();
        place.category = obj["category"].toString();
        place.coordinates = QPointF(obj["x"].toDouble(), obj["y"].toDouble());
        places.append(place);
    }

    // 确保 adjMatrix 的大小与 places 数量一致
    // 确保 adjMatrix 的大小与 places 数量一致
    int numPlaces = places.size();
    adjMatrix.clear();
    for (int i = 1; i <= numPlaces; ++i) {
        if (!adjMatrix.contains(i)) {
            adjMatrix[i] = QMap<int, double>();  // 手动初始化每个 QMap
        }
    }


    // 加载路径信息
    // 加载路径信息
    QJsonArray pathsArray = root["paths"].toArray();
    for (const QJsonValue& value : pathsArray) {
        QJsonObject obj = value.toObject();
        int from = obj["from"].toInt();
        int to = obj["to"].toInt();
        double weight = obj["weight"].toDouble();

        // 更新邻接矩阵（双向道路）
        if (from != to) {
            adjMatrix[from][to] = weight;
            adjMatrix[to][from] = weight;
        }
    }

}

void CampusMap::saveMapToJson(const QString& filePath) {
    QJsonObject root;

    // 保存场所信息
    QJsonArray placesArray;
    for (const Place& place : places) {
        QJsonObject obj;
        obj["id"] = place.id;
        obj["name"] = place.name;
        obj["category"] = place.category;
        obj["x"] = place.coordinates.x();
        obj["y"] = place.coordinates.y();
        placesArray.append(obj);
    }
    root["places"] = placesArray;

    // 保存路径信息
    QJsonArray pathsArray;
    for (auto from : adjMatrix.keys()) {
        for (auto to : adjMatrix[from].keys()) {
            if (from < to) { // 避免重复存储双向道路
                QJsonObject obj;
                obj["from"] = from;
                obj["to"] = to;
                obj["weight"] = adjMatrix[from][to];
                pathsArray.append(obj);
            }
        }
    }
    root["paths"] = pathsArray;

    QJsonDocument doc(root);
    QFile file(filePath);
    if (!file.open(QIODevice::WriteOnly | QIODevice::Text)) {
        return;
    }

    file.write(doc.toJson());
}

double CampusMap::calculateDistance(const QPointF& a, const QPointF& b) {
    return std::sqrt(std::pow(a.x() - b.x(), 2) + std::pow(a.y() - b.y(), 2));
}

// 使用改进的 Dijkstra 算法求最短路径
using Pair = std::pair<double, int>;

QVector<int> CampusMap::shortestPath(int startPlaceId, int endPlaceId) {
    if (startPlaceId < 1 || startPlaceId > places.size() || endPlaceId < 1 || endPlaceId > places.size()) {
        return {};  // 无效输入
    }

    int n = places.size();
    QVector<double> dist(n + 1, std::numeric_limits<double>::infinity());
    QVector<int> prev(n + 1, -1);
    QVector<bool> visited(n + 1, false);

    dist[startPlaceId] = 0.0;

    // 使用 std::priority_queue
    std::priority_queue<Pair, std::vector<Pair>, std::greater<Pair>> pq;
    pq.push({ 0.0, startPlaceId }); // 将起点插入队列

    while (!pq.empty()) {
        // 从队列中取出距离最近的节点
        Pair current = pq.top();
        pq.pop();
        double currentDist = current.first;
        int u = current.second;

        if (visited[u]) continue;
        visited[u] = true;

        if (u == endPlaceId) break;

        // 遍历当前节点的邻接点
        for (auto it = adjMatrix[u].cbegin(); it != adjMatrix[u].cend(); ++it) {
            int v = it.key();
            QPointF coordU = places[u - 1].coordinates;
            QPointF coordV = places[v - 1].coordinates;
            double weight = calculateDistance(coordU, coordV);

            // 如果找到更短的路径
            if (!visited[v] && currentDist + weight < dist[v]) {
                dist[v] = currentDist + weight;
                prev[v] = u;
                pq.push({ dist[v], v });
            }
        }
    }

    // 生成最短路径
    QVector<int> path;
    for (int u = endPlaceId; u != -1; u = prev[u]) {
        path.prepend(u);
    }

    if (path.isEmpty() || path.first() != startPlaceId) {
        return {};
    }
    return path;
}

// 检测两条道路是否相交
bool CampusMap::lineIntersect(const QPointF& A1, const QPointF& A2, const QPointF& B1, const QPointF& B2, QPointF& intersection) {
    double denom = (A1.x() - A2.x()) * (B1.y() - B2.y()) - (A1.y() - A2.y()) * (B1.x() - B2.x());
    if (denom == 0) {
        return false; // 平行或重合，不相交
    }

    double t = ((A1.x() - B1.x()) * (B1.y() - B2.y()) - (A1.y() - B1.y()) * (B1.x() - B2.x())) / denom;
    double u = ((A1.x() - B1.x()) * (A1.y() - A2.y()) - (A1.y() - B1.y()) * (A1.x() - A2.x())) / denom;

    if (t >= 0 && t <= 1 && u >= 0 && u <= 1) {
        intersection = QPointF(A1.x() + t * (A2.x() - A1.x()), A1.y() + t * (A2.y() - A1.y()));
        return true;
    }

    return false;
}

// 分割相交的道路
QVector<QPair<int, int>> CampusMap::splitRoadAtIntersection(int from, int to, const QPointF& intersection) {
    QVector<QPair<int, int>> newRoads;

    QPointF fromCoord = places[from - 1].coordinates;
    QPointF toCoord = places[to - 1].coordinates;

    double distFromIntersection = calculateDistance(fromCoord, intersection);
    double distToIntersection = calculateDistance(toCoord, intersection);

    int newTo = places.size() + 1;
    double weight1 = distFromIntersection;
    adjMatrix[from][newTo] = weight1;
    adjMatrix[newTo][from] = weight1;

    double weight2 = distToIntersection;
    adjMatrix[newTo][to] = weight2;
    adjMatrix[to][newTo] = weight2;

    newRoads.append(qMakePair(from, newTo));
    newRoads.append(qMakePair(newTo, to));

    return newRoads;
}
