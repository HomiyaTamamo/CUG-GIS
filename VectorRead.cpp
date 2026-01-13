#include "VectorRead.h"

VectorRead::VectorRead() : mpoDataset(nullptr), mbIsValid(false) {}

VectorRead::~VectorRead() {
    close();
}

void VectorRead::close() {
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

bool VectorRead::isValid() const {
    return mbIsValid;
}

std::vector<OGRGeometry*>& VectorRead::getGeometries() {
    return mvGeometries;
}

