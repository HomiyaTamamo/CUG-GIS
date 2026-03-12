#pragma once

#include <QtWidgets/QMainWindow>
#include <QVBoxLayout>
#include <QPushButton>
#include <QMessageBox>
#include <qgsvectorlayer.h>
#include <qgsproject.h>
#include <qgsmapcanvas.h>
#include <qgsfeature.h>
#include <qgsgeometry.h>
#include <vector>
#include <fstream>


struct Coordinate {
    double x;
    double y;
};

class gisaboutxy : public QMainWindow {
    Q_OBJECT

public:
    gisaboutxy(QWidget* parent = nullptr);
    ~gisaboutxy();

private slots:
    void onLoadFileClicked();
    void onSaveFileClicked();

private:
    QWidget* centralWidget;         // 中心控件
    QVBoxLayout* mainLayout;        // 布局
    QPushButton* loadFileButton;    // 加载文件按钮
    QPushButton* saveFileButton;    // 保存文件按钮
    QgsMapCanvas* mapCanvas;        // 地图画布
    QgsVectorLayer* vecLayer;       // 点矢量图层

    void displayCoordinatesOnMap(const std::vector<Coordinate>& coordinates);
    std::vector<Coordinate> readCoordinatesFromFile(const QString& filePath);
};
