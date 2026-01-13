#ifndef SHAPEFILEREAD_H
#define SHAPEFILEREAD_H

#include <ogrsf_frmts.h>
#include <vector>
#include <string>

class ShapefileRead
{
public:
    ShapefileRead();
    ~ShapefileRead();

    bool loadFrom(const char* pszFilename);
    std::vector<OGRGeometry*>& getGeometries();

private:
    void close();
    void logEvent(const std::string& event);

    GDALDataset* mpoDataset;
    bool mbIsValid;
    std::vector<OGRGeometry*> mvGeometries;
};

#endif // SHAPEFILEREAD_H
