#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QMessageBox>
#include <QFileDialog>
#include <QGraphicsPixmapItem>
#include <cmath>
#include "zoomgraphicsview.h"
#include "rasterdisplayview.h"
#include <ogrsf_frmts.h>
#include <QDebug>
#include <string>
#include "ShapefileRead.h"  

MainWindow::MainWindow(QWidget* parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow),
    ImageItem(nullptr)
{
    ui->setupUi(this);
    ui->graphicsView->setScene(&scene);
    ui->graphicsView->setDragMode(QGraphicsView::ScrollHandDrag);

    connect(ui->graphicsView, SIGNAL(mouseCoordChanged(int, int, float)), this, SLOT(updateCoords(int, int, float)));
    connect(ui->graphicsView, SIGNAL(mouseHitPoint(int, int, int)), this, SLOT(hitCircle(int, int, int)));
   
}

MainWindow::~MainWindow() {
    delete ui;
}

void MainWindow::on_actionOpen_New_Image_triggered() {
    if (maybeSave()) {
        QString fileName = QFileDialog::getOpenFileName(this, "Open New Image File", tr("/home"), tr("Image Files (*.tif *.tiff *.shp)"));
        if (!fileName.isEmpty()) {
            if (fileName.endsWith(".tif") || fileName.endsWith(".tiff")) {
                openRasterFile(fileName);
            }
            else if (fileName.endsWith(".shp")) {
                openVectorFile(fileName);
            }
            else {
                loadFile(fileName);
            }
        }
    }
}



void MainWindow::on_actionDowdload_triggered()
{

}

void MainWindow::on_actionAbout_triggered() {
    QMessageBox::about(this, "Test", tr("Image Viewer and Inspector\n by GRAM  (C)2017"));
}

void MainWindow::updateCoords(int iX, int iY, float fZoomFactor) {
    QString szInfo = QString("x = %1, y = %2\nZoom = %3").arg(iX).arg(iY).arg(fZoomFactor, 0, 'f', 3);
    ui->textBrowser->setText(szInfo);
}

int MainWindow::maybeSave() {
    return true;
}

int MainWindow::loadFile(QString szFileName) {
    int iRet = true;
    if (!szFileName.isEmpty()) {
        image = QImage(szFileName);
        if (image.isNull()) {
            QMessageBox::information(this, tr("Image Viewer"), tr("Cannot load %1.").arg(szFileName));
            iRet = false;
        }
        else {
            ui->lblFileName->setText(szFileName);
            QString szFileInfo;
            szFileInfo = QString("Image size: x = %1  y = %2").arg(image.width()).arg(image.height());

            ui->lblFileInfo->setText(szFileInfo);

            dirtyPixmap(&image);

            scene.clear();
            scene.setSceneRect(0, 0, image.width(), image.height());
            ImageItem = scene.addPixmap(QPixmap::fromImage(image));
            scene.update();

            ui->graphicsView->resetZoom();
        }
    }
    return iRet;
}

int MainWindow::dirtyPixmap(QImage* pixMap) {
    if (!pixMap) {
        return false;
    }

    int iType = pixMap->depth();

    for (int i = 0; i < 720; i++) {
        pixMap->setPixel(100 + 40 * cos(i / 180.0 * 3.1415 * 0.5), 100 + 40 * sin(i / 180.0 * 3.1415 * 0.5), 0xFF0000);
    }

    scene.update();
    return true;
}

void MainWindow::hitCircle(int iX, int iY, int iAction) {
    if (!image.isNull()) {
        for (int i = 0; i < 720; i++) {
            image.setPixel(iX + 40 * cos(i / 180.0 * 3.1415 * 0.5), iY + 40 * sin(i / 180.0 * 3.1415 * 0.5), 0xFF0000);
        }

        ImageItem->setPixmap(QPixmap::fromImage(image));
        scene.update();
    }
    else
        return;
}

void MainWindow::on_actionZoom_Reset_triggered() {
    ui->graphicsView->resetZoom();
}

void MainWindow::openRasterFile(const QString& filePath) {
    RasterDisplayView rasterView;
    QImage rasterImage = rasterView.rasterToImage(filePath);
    if (!rasterImage.isNull()) {
        image = rasterImage;
        scene.clear();
        scene.setSceneRect(0, 0, image.width(), image.height());
        ImageItem = scene.addPixmap(QPixmap::fromImage(image));
        scene.update();
        ui->graphicsView->resetZoom();
    }
    else {
        QMessageBox::warning(this, tr("Error"), tr("Failed to load the raster image."));
    }
}

void MainWindow::openVectorFile(const QString& filePath)
{
    ShapefileRead shapefileReader;

    // 将QString转换为const char*
    QByteArray byteArray = filePath.toLocal8Bit();
    const char* fixedPathConstChar = byteArray.constData();

    qDebug() << "File path: " << filePath;

    if (!shapefileReader.loadFrom(fixedPathConstChar)) {
        QMessageBox::warning(this, tr("Error"), tr("Failed to load the shapefile image"));
        return;
    }

    // 清除现有场景内容
    scene.clear();

    // 获取矢量文件中的几何体并创建对应的 GeometryItem 对象添加到场景中
    std::vector<OGRGeometry*>& geometries = shapefileReader.getGeometries();
    for (auto geometry : geometries) {
        GeometryItem* item = new GeometryItem(geometry);
        scene.addItem(item);
    }

    // 更新场景
    scene.update();
    ui->graphicsView->resetZoom();
}



