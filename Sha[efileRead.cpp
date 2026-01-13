#include "ShapefileRead.h"
#include <qdebug.h>
#include <fstream>
#include <chrono>
#include <ctime>

ShapefileRead::ShapefileRead() : mpoDataset(nullptr), mbIsValid(false) {
    logEvent("Program opened");
}

ShapefileRead::~ShapefileRead() {
    logEvent("Program closed");
    close();
}

bool ShapefileRead::loadFrom(const char* pszFilename) {
    close();

    GDALAllRegister();
    OGRRegisterAll();
    CPLSetConfigOption("GDAL_FILENAME_IS_UTF8", "NO");

    mpoDataset = static_cast<GDALDataset*>(GDALOpenEx(pszFilename, GDAL_OF_VECTOR, nullptr, nullptr, nullptr));
    if (mpoDataset == nullptr) {
        qDebug() << "ShapefileRead::loadFrom : open file error!";
        return false;
    }

    OGRLayer* poLayer = mpoDataset->GetLayer(0);
    if (poLayer == nullptr) {
        qDebug() << "ShapefileRead::loadFrom : get layer error!";
        close();
        return false;
    }

    OGRFeature* poFeature;
    poLayer->ResetReading();
    while ((poFeature = poLayer->GetNextFeature()) != nullptr) {
        OGRGeometry* poGeometry = poFeature->StealGeometry();
        if (poGeometry != nullptr) {
            mvGeometries.push_back(poGeometry);
        }
        OGRFeature::DestroyFeature(poFeature);
    }

    mbIsValid = true;
    return true;
}

void ShapefileRead::close() {
    if (mpoDataset != nullptr) {
        GDALClose(mpoDataset);
        mpoDataset = nullptr;
    }

    for (auto& poGeom : mvGeometries) {
        OGRGeometryFactory::destroyGeometry(poGeom);
    }
    mvGeometries.clear();
    mbIsValid = false;
}

std::vector<OGRGeometry*>& ShapefileRead::getGeometries() {
    return mvGeometries;
}

void ShapefileRead::logEvent(const std::string& event) {
    std::ofstream logFile("log.txt", std::ios_base::app);
    if (logFile.is_open()) {
        auto now = std::chrono::system_clock::now();
        auto in_time_t = std::chrono::system_clock::to_time_t(now);
        logFile << std::ctime(&in_time_t) << ": " << event << std::endl;
    }
}
