#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include <QGraphicsScene>
#include <QGraphicsPixmapItem>
#include <QImage>
#include "GeometryItem.h"
#include "ShapefileRead.h"

namespace Ui {
    class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget* parent = nullptr);
    ~MainWindow();

private slots:
    void on_actionOpen_New_Image_triggered();
    void on_actionDowdload_triggered();
    void on_actionAbout_triggered();
    void on_actionZoom_Reset_triggered();
    void updateCoords(int iX, int iY, float fZoomFactor);
    void hitCircle(int iX, int iY, int iAction);


private:
    Ui::MainWindow* ui;
    QGraphicsScene scene;
    QImage image;
    QGraphicsPixmapItem* ImageItem;
    ShapefileRead shapefilereader;
    QList<QGraphicsItem*> geometryItems;


    int maybeSave();
    int loadFile(QString szFileName);
    int dirtyPixmap(QImage* pixMap);
    void openRasterFile(const QString& filePath);
    void openVectorFile(const QString& filePath);
};

#endif // MAINWINDOW_H



