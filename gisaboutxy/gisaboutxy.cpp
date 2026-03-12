#include "gisaboutxy.h"
#include <qfiledialog.h>
#include <qgsvectorfilewriter.h>
#include <qgsvectorlayerexporter.h>

gisaboutxy::gisaboutxy(QWidget* parent)
    : QMainWindow(parent),
    centralWidget(new QWidget(this)),
    mainLayout(new QVBoxLayout(centralWidget)),
    loadFileButton(new QPushButton("Open File", centralWidget)),
    saveFileButton(new QPushButton("Save File", centralWidget)),
    mapCanvas(new QgsMapCanvas(centralWidget)),
    vecLayer(nullptr) {

    // 设置窗口标题
    setWindowTitle("GIS About XY");

    // 配置地图画布
    mapCanvas->setCanvasColor(Qt::white);
    mapCanvas->setMinimumHeight(400);

    // 添加控件到布局
    mainLayout->addWidget(loadFileButton);
    mainLayout->addWidget(saveFileButton);
    mainLayout->addWidget(mapCanvas);

    // 设置中心控件
    setCentralWidget(centralWidget);

    // 连接按钮信号到槽
    connect(loadFileButton, &QPushButton::clicked, this, &gisaboutxy::onLoadFileClicked);
    connect(saveFileButton, &QPushButton::clicked, this, &gisaboutxy::onSaveFileClicked);
}

gisaboutxy::~gisaboutxy() {}

void gisaboutxy::onLoadFileClicked() {
    // 打开文件对话框，选择坐标文件
    QString filePath = QFileDialog::getOpenFileName(this, "Open Coordinate File", "", "Text Files (*.txt)");
    if (filePath.isEmpty()) return;

    // 读取坐标
    auto coordinates = readCoordinatesFromFile(filePath);
    if (coordinates.empty()) {
        QMessageBox::warning(this, "Warning", "No valid coordinates found in the file.");
        return;
    }

    // 显示到地图
    displayCoordinatesOnMap(coordinates);
}

void gisaboutxy::onSaveFileClicked() {
    if (!vecLayer) {
        QMessageBox::warning(this, "Warning", "No data to save.");
        return;
    }

    // 打开文件对话框，选择保存路径
    QString filePath = QFileDialog::getSaveFileName(this, "Save as Shapefile", "", "Shapefiles (*.shp)");
    if (filePath.isEmpty()) return;

    // 使用 QgsVectorFileWriter 保存图层为 Shapefile
    QgsVectorFileWriter::writeAsVectorFormat(vecLayer, filePath, "UTF-8", vecLayer->crs(), "ESRI Shapefile");
    QMessageBox::information(this, "Success", "Shapefile saved successfully.");
}

std::vector<Coordinate> gisaboutxy::readCoordinatesFromFile(const QString& filePath) {
    std::vector<Coordinate> coordinates;
    std::ifstream file(filePath.toStdString());
    if (!file.is_open()) {
        QMessageBox::critical(this, "Error", "Failed to open the file.");
        return coordinates;
    }

    double x, y;
    while (file >> x >> y) {
        coordinates.push_back({ x, y });
    }
    file.close();
    return coordinates;
}

void gisaboutxy::displayCoordinatesOnMap(const std::vector<Coordinate>& coordinates) {
    // 创建临时图层
    vecLayer = new QgsVectorLayer("Point?crs=EPSG:4326", "Coordinates", "memory");
    if (!vecLayer->isValid()) {
        QMessageBox::critical(this, "Error", "Failed to create vector layer.");
        return;
    }

    auto provider = vecLayer->dataProvider();
    for (const auto& coord : coordinates) {
        QgsFeature feature;
        feature.setGeometry(QgsGeometry::fromPointXY(QgsPointXY(coord.x, coord.y)));
        provider->addFeature(feature);
    }

    // 添加图层到项目并刷新地图
    QgsProject::instance()->addMapLayer(vecLayer);
    mapCanvas->setLayers({ vecLayer });
    mapCanvas->refresh();
}
