#include "MainWindow.h"
#include "ClickableEllipseItem.h"  // 引入自定义类
#include <QFileDialog>
#include <QGraphicsEllipseItem>
#include <QGraphicsTextItem>
#include <QInputDialog>
#include <QVBoxLayout>
#include <QHBoxLayout>
#include <QMessageBox>

MainWindow::MainWindow(QWidget* parent)
    : QMainWindow(parent), scene(new QGraphicsScene(this)), startPlaceId(-1), endPlaceId(-1), startSelected(false) {

    // 设置主窗口布局
    QWidget* centralWidget = new QWidget(this);
    layout = new QVBoxLayout(centralWidget);

    // 图形视图
    view = new QGraphicsView(scene, centralWidget);
    view->setMinimumSize(800, 600);
    layout->addWidget(view);

    // 按钮
    loadButton = new QPushButton("Load Map", centralWidget);
    saveButton = new QPushButton("Save Map", centralWidget);
    queryButton = new QPushButton("Query Place", centralWidget);

    // 按钮布局
    QHBoxLayout* buttonLayout = new QHBoxLayout;
    buttonLayout->addWidget(loadButton);
    buttonLayout->addWidget(saveButton);
    buttonLayout->addWidget(queryButton);
    layout->addLayout(buttonLayout);

    // 连接信号和槽
    connect(loadButton, &QPushButton::clicked, this, &MainWindow::loadMap);
    connect(saveButton, &QPushButton::clicked, this, &MainWindow::saveMap);
    connect(queryButton, &QPushButton::clicked, this, &MainWindow::queryPlace);

    // 设置中心窗口
    setCentralWidget(centralWidget);
}

MainWindow::~MainWindow() {
    delete scene;
}

void MainWindow::loadMap() {
    QString filePath = QFileDialog::getOpenFileName(this, "Load Map", "", "JSON Files (*.json)");
    if (!filePath.isEmpty()) {
        campusMap.loadMapFromJson(filePath);
        updateView();
    }
}

void MainWindow::saveMap() {
    QString filePath = QFileDialog::getSaveFileName(this, "Save Map", "", "JSON Files (*.json)");
    if (!filePath.isEmpty()) {
        campusMap.saveMapToJson(filePath);
    }
}

void MainWindow::queryPlace() {
    bool ok;
    int placeId = QInputDialog::getInt(this, "Query Place", "Enter Place ID:", 1, 1, campusMap.places.size(), 1, &ok);
    if (ok) {
        for (const Place& place : campusMap.places) {
            if (place.id == placeId) {
                QString info = QString("ID: %1\nName: %2\nCategory: %3\nCoordinates: (%4, %5)")
                    .arg(place.id)
                    .arg(place.name)
                    .arg(place.category)
                    .arg(place.coordinates.x())
                    .arg(place.coordinates.y());
                QMessageBox::information(this, "Place Information", info);
                return;
            }
        }
        QMessageBox::warning(this, "Query Error", "Place not found!");
    }
}

void MainWindow::onPlaceClicked(int placeId) {
    if (!startSelected) {
        // 选择起点
        startPlaceId = placeId;
        startSelected = true;
        QMessageBox::information(this, "Start Point Selected", "Start point selected: " + QString::number(placeId));
    } else {
        // 选择终点
        endPlaceId = placeId;
        startSelected = false;
        QMessageBox::information(this, "End Point Selected", "End point selected: " + QString::number(placeId));
        findShortestPath();
    }
}

void MainWindow::findShortestPath() {
    if (startPlaceId == -1 || endPlaceId == -1) {
        QMessageBox::warning(this, "Path Error", "Please select both start and end points.");
        return;
    }

    QVector<int> path = campusMap.shortestPath(startPlaceId, endPlaceId);
    if (path.isEmpty()) {
        QMessageBox::warning(this, "Path Error", "No path found!");
        return;
    }

    // 高亮显示路径
    highlightPath(path);
}

void MainWindow::highlightPath(const QVector<int>& path) {
    // 清空之前的图形显示
    scene->clear();
    updateView(); // 重新绘制地图

    // 绘制路径为红色
    for (int i = 0; i < path.size() - 1; ++i) {
        int fromId = path[i];
        int toId = path[i + 1];
        QPointF fromCoord, toCoord;
        for (const Place& place : campusMap.places) {
            if (place.id == fromId) fromCoord = place.coordinates;
            if (place.id == toId) toCoord = place.coordinates;
        }

        // 使用红色高亮路径
        scene->addLine(fromCoord.x(), fromCoord.y(), toCoord.x(), toCoord.y(), QPen(Qt::red, 2));
    }
}

void MainWindow::updateView() {
    // 清空原有内容
    scene->clear();

    // 绘制建筑物
    for (const Place& place : campusMap.places) {
        ClickableEllipseItem* node = new ClickableEllipseItem(place.id);
        node->setRect(place.coordinates.x() - 5, place.coordinates.y() - 5, 10, 10);
        node->setBrush(QBrush(Qt::blue));
        node->setPen(QPen(Qt::black));
        scene->addItem(node);
        
        // 建筑物名称
        QGraphicsTextItem* nameItem = scene->addText(place.name);
        nameItem->setPos(place.coordinates.x() + 10, place.coordinates.y());
        
        // 连接信号和槽
        connect(node, &ClickableEllipseItem::clicked, this, &MainWindow::onPlaceClicked);
    }

    // 绘制道路（连接建筑物）
    for (auto from : campusMap.adjMatrix.keys()) {
        for (auto to : campusMap.adjMatrix[from].keys()) {
            QPointF p1, p2;
            for (const Place& place : campusMap.places) {
                if (place.id == from) p1 = place.coordinates;
                if (place.id == to) p2 = place.coordinates;
            }
            scene->addLine(p1.x(), p1.y(), p2.x(), p2.y(), QPen(Qt::gray));
        }
    }
}
