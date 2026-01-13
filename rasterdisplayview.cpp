#include "rasterdisplayview.h"
#include "gdal_priv.h"
#include "cpl_conv.h" // for CPLMalloc()

RasterDisplayView::RasterDisplayView(QWidget* parent)
{
    GDALAllRegister();
}

QImage RasterDisplayView::rasterToImage(const QString& filePath)
{
    GDALDataset* poDataset = (GDALDataset*)GDALOpen(filePath.toStdString().c_str(), GA_ReadOnly);
    if (poDataset == nullptr) {
        return QImage();
    }

    int nBands = poDataset->GetRasterCount();
    if (nBands < 3) {  // 确保有至少三个波段
        GDALClose(poDataset);
        return QImage();
    }

    GDALRasterBand* poBandR = poDataset->GetRasterBand(1);
    GDALRasterBand* poBandG = poDataset->GetRasterBand(2);
    GDALRasterBand* poBandB = poDataset->GetRasterBand(3);

    int nXSize = poBandR->GetXSize();
    int nYSize = poBandR->GetYSize();

    QImage image(nXSize, nYSize, QImage::Format_RGB888);

    for (int y = 0; y < nYSize; y++) {
        uchar* scanLineR = (uchar*)CPLMalloc(sizeof(uchar) * nXSize);
        uchar* scanLineG = (uchar*)CPLMalloc(sizeof(uchar) * nXSize);
        uchar* scanLineB = (uchar*)CPLMalloc(sizeof(uchar) * nXSize);

        poBandR->RasterIO(GF_Read, 0, y, nXSize, 1, scanLineR, nXSize, 1, GDT_Byte, 0, 0);
        poBandG->RasterIO(GF_Read, 0, y, nXSize, 1, scanLineG, nXSize, 1, GDT_Byte, 0, 0);
        poBandB->RasterIO(GF_Read, 0, y, nXSize, 1, scanLineB, nXSize, 1, GDT_Byte, 0, 0);

        for (int x = 0; x < nXSize; x++) {
            image.setPixel(x, y, qRgb(scanLineR[x], scanLineG[x], scanLineB[x]));
        }

        CPLFree(scanLineR);
        CPLFree(scanLineG);
        CPLFree(scanLineB);
    }

    GDALClose(poDataset);
    return image;
}


