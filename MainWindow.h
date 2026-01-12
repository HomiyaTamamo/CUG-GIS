#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QGraphicsView>
#include <QGraphicsScene>
#include <QPushButton>
#include <QVBoxLayout>
#include <QGraphicsEllipseItem>
#include <QGraphicsTextItem>
#include <QVector>
#include "CampusMap.h"  

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget* parent = nullptr);
    ~MainWindow();

private slots:
    void loadMap();             // 加载地图
    void saveMap();             // 保存地图
    void queryPlace();          // 查询建筑物信息
    void onPlaceClicked(int placeId); // 处理建筑物点击事件
    void findShortestPath();    // 查找最短路径

private:
    void updateView();          // 更新视图
    void highlightPath(const QVector<int>& path); // 高亮显示最短路径

    QVector<int> dijkstra(int startId, int endId);

    QWidget* centralWidget;      // 中心窗口部件
    QVBoxLayout* layout;         // 布局管理器
    QGraphicsView* view;        // 图形视图
    QGraphicsScene* scene;      // 图形场景

    QPushButton* loadButton;    // 加载按钮
    QPushButton* saveButton;    // 保存按钮
    QPushButton* queryButton;   // 查询按钮

    CampusMap campusMap;        // 校园地图对象，假设有CampusMap类处理地图数据

    int startPlaceId;           // 起点建筑物ID
    int endPlaceId;             // 终点建筑物ID
    bool startSelected;         // 是否已选定起点
};

#endif // MAINWINDOW_H
