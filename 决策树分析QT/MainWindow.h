#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <map>
#include <QFileDialog>
#include <QLabel>
#include <QLineEdit>
#include <QMainWindow>
#include <QMessageBox>
#include <QPushButton>
#include <QStandardItemModel>
#include <QString>
#include <QTableView>
#include <string>
#include <vector>
#include <qapplication.h>
#include <qgraphicsscene.h>
#include <qgraphicsview.h>  


class TreeNode;

class MainWindow : public QMainWindow {
    Q_OBJECT

public:
    explicit MainWindow(QWidget* parent = nullptr);
    ~MainWindow();
    QString currentFilename;
    std::string currFilename = currentFilename.toUtf8().constData();

private slots:
    void onLoadButtonClicked();
    void drawTreeInView(TreeNode* root);
    void drawTree(TreeNode* root, QGraphicsScene* scene, int x, int y, int level);
    void onSaveButtonClicked();
    void onPredictButtonClicked();
    void onShowWeightsButtonClicked();

private:
    void loadCSVToTable(const QString& filename);
    void saveData();
    void clearTable();

    // CSVÏÔÊ¾ÇøÓò
    QTableView* tableView;
    QStandardItemModel* tableModel;
    QLabel* resultLabel;
    QLineEdit* lineEdit;
    QGraphicsView* treeView;  

signals:
    void updatePredictionResult(const QString& result);

public slots:
    void onUpdatePredictionResult(const QString& result) {
        resultLabel->setText(result);
    }
};

#endif // MAINWINDOW_H
