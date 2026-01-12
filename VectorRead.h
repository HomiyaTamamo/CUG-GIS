#ifndef VECTORREAD_H
#define VECTORREAD_H

#include "gdal_priv.h"
#include "ogrsf_frmts.h"
#include <vector>

class VectorRead {
public:
    VectorRead();
    virtual ~VectorRead();

    virtual bool loadFrom(const char* filename) = 0;
    virtual void close();
    bool isValid() const;

    std::vector<OGRGeometry*>& getGeometries();

protected:
    GDALDataset* mpoDataset;
    std::vector<OGRGeometry*> mvGeometries;
    bool mbIsValid;
};

#endif // VECTORREAD_H

