#ifndef RASTERDISPLAYVIEW_H
#define RASTERDISPLAYVIEW_H

#include <QImage>
#include <QString>

class RasterDisplayView
{
public:
    RasterDisplayView(QWidget* parent = nullptr);
    QImage rasterToImage(const QString& filePath);
};

#endif // RASTERDISPLAYVIEW_H
