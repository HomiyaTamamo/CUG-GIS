#include "mainwindow.h"
#include <QVBoxLayout>
#include <QHBoxLayout>
#include <QFileDialog>
#include <QMessageBox>
#include <QTextStream>
#include <QStandardItem>
#include <QStandardItemModel>
#include <QDebug>
#include <set>
#include "decisiontree.h"
#include <sstream>
#include<string>
#include <QSplitter>
#include <QGraphicsTextItem>

MainWindow::MainWindow(QWidget* parent)
    : QMainWindow(parent), tableModel(new QStandardItemModel(this)) {
    setWindowTitle("Decision Tree Visualization");
    QWidget* centralWidget = new QWidget(this);
    QHBoxLayout* mainLayout = new QHBoxLayout(centralWidget);
    QWidget* leftWidget = new QWidget(this);
    QVBoxLayout* leftLayout = new QVBoxLayout(leftWidget);
    tableView = new QTableView(this);
    tableView->setModel(tableModel);
    leftLayout->addWidget(tableView);
    QPushButton* loadButton = new QPushButton("Load CSV and Train", this);
    leftLayout->addWidget(loadButton);
    QPushButton* saveButton = new QPushButton("Save CSV", this);
    leftLayout->addWidget(saveButton);
    QPushButton* showWeightsButton = new QPushButton("Show Weights", this);
    leftLayout->addWidget(showWeightsButton);
    QHBoxLayout* inputLayout = new QHBoxLayout();
    inputLayout->addWidget(new QLabel("Enter data for prediction:"));
    lineEdit = new QLineEdit(this);
    inputLayout->addWidget(lineEdit);
    QPushButton* predictButton = new QPushButton("Predict", this);
    inputLayout->addWidget(predictButton);
    leftLayout->addLayout(inputLayout);
    resultLabel = new QLabel("Prediction result: ", this);
    leftLayout->addWidget(resultLabel);
    leftWidget->setLayout(leftLayout);
    QWidget* rightWidget = new QWidget(this);
    QVBoxLayout* rightLayout = new QVBoxLayout(rightWidget);
    treeView = new QGraphicsView(this);
    treeView->setRenderHint(QPainter::Antialiasing);
    rightLayout->addWidget(treeView);
    rightWidget->setLayout(rightLayout);
    QSplitter* splitter = new QSplitter(Qt::Horizontal);
    splitter->addWidget(leftWidget);
    //splitter->addWidget(rightWidget);  // 做了没用上
    splitter->setStretchFactor(0, 1);
    splitter->setStretchFactor(1, 1);
    mainLayout->addWidget(splitter);
    centralWidget->setLayout(mainLayout);
    setCentralWidget(centralWidget);

    connect(loadButton, &QPushButton::clicked, this, &MainWindow::onLoadButtonClicked);
    connect(saveButton, &QPushButton::clicked, this, &MainWindow::onSaveButtonClicked);
    connect(predictButton, &QPushButton::clicked, this, &MainWindow::onPredictButtonClicked);
    connect(showWeightsButton, &QPushButton::clicked, this, &MainWindow::onShowWeightsButtonClicked);
}



MainWindow::~MainWindow() {}

// 加载 CSV 文件
void MainWindow::onLoadButtonClicked() {
    QString filename = QFileDialog::getOpenFileName(this, "Open CSV File", "", "CSV Files (*.csv);;All Files (*)");
    if (filename.isEmpty()) {
        return;
    }

    // 加载数据并更新表格
    loadCSVToTable(filename);

    // 加载数据并构建决策树
    std::vector<Data> dataset = loadData(filename.toStdString());
    std::set<std::string> usedAttributes;
    TreeNode* root = buildTree(dataset, usedAttributes);  // 创建决策树

    // 创建 QGraphicsScene 用于绘制树
    QGraphicsScene* scene = new QGraphicsScene(this);

    // 设置场景的可视区域，确保树不会超出区域
    scene->setSceneRect(0, 0, 800, 600);  // 调整场景的显示区域，可以根据需要调整

    // 在 QGraphicsScene 中绘制决策树
    drawTree(root, scene, 400, 50, 0);  // 将树的根节点放在视图的中心

    // 将场景设置到 QGraphicsView 中
    treeView->setScene(scene);  // 将绘制的场景添加到 treeView（右侧区域的 QGraphicsView）

    // 调整 QGraphicsView 的视图，确保它适应场景
    treeView->setRenderHint(QPainter::Antialiasing);
    treeView->fitInView(scene->sceneRect(), Qt::KeepAspectRatio);  // 自动缩放，保持纵横比

    // 显示视图窗口（树的部分）
    treeView->show();
}


// 绘制树到右侧区域的函数
void MainWindow::drawTreeInView(TreeNode* root) {
    // 创建 QGraphicsScene 用于绘制树
    QGraphicsScene* scene = new QGraphicsScene(this);

    // 设置场景的可视区域，假设树不会非常大
    scene->setSceneRect(0, 0, 800, 600);  // 设置场景显示区域，可以根据需要调整

    // 在 QGraphicsScene 中绘制决策树
    drawTree(root, scene, 50, 50, 0);  // 在场景中绘制树

    // 将场景设置到右侧区域的 QGraphicsView 中
    treeView->setScene(scene);

    // 显示视图窗口（树的部分）
    treeView->show();
}

// 绘制树的具体实现，但显示不出来，删了还报错，就放这里了
void MainWindow::drawTree(TreeNode* root, QGraphicsScene* scene, int x, int y, int level) {
    if (!root) return;  // 如果当前节点为空，则退出

    // 设置字体并增大节点文本
    QFont font = scene->font();
    font.setPointSize(16);  // 增大字体大小

    // 计算文本的尺寸
    QGraphicsTextItem* nodeText = scene->addText(QString::fromStdString(root->attribute), font);  // 使用节点的属性作为文本
    QRectF textRect = nodeText->boundingRect();  // 获取文本的矩形区域
    nodeText->setPos(x - textRect.width() / 2, y - textRect.height() / 2);  // 将文本居中在节点上

    // 绘制圆形包围节点
    int radius = std::max(textRect.width(), textRect.height()) / 2 + 10;  // 圆的半径，稍微大于文本
    scene->addEllipse(x - radius, y - radius, radius * 2, radius * 2, QPen(Qt::black), QBrush(Qt::lightGray));  // 绘制圆形背景

    // 如果是叶子节点，显示叶子节点标签
    if (root->isLeaf) {
        nodeText->setPlainText(QString::fromStdString(root->label));  // 如果是叶子节点，显示标签
    }

    // 绘制边（连接父节点和子节点）
    int childY = y + 100;  // 子节点的Y坐标，增大Y坐标的间隔
    int childXLeft = x - 150;  // 左子节点的X坐标，增大X坐标的间隔
    int childXRight = x + 150;  // 右子节点的X坐标，增大X坐标的间隔

    // 如果存在左子节点，绘制从父节点到左子节点的连线，并递归调用绘制左子树
    if (root->children.find("left") != root->children.end()) {
        scene->addLine(x, y, childXLeft, childY);  // 绘制左子节点的连线
        drawTree(root->children.at("left"), scene, childXLeft, childY, level + 1);  // 递归绘制左子树
    }

    // 如果存在右子节点，绘制从父节点到右子节点的连线，并递归调用绘制右子树
    if (root->children.find("right") != root->children.end()) {
        scene->addLine(x, y, childXRight, childY);  // 绘制右子节点的连线
        drawTree(root->children.at("right"), scene, childXRight, childY, level + 1);  // 递归绘制右子树
    }
}

// 保存 CSV 文件
void MainWindow::onSaveButtonClicked() {
    saveData();
}

// 加载 CSV 数据到表格
void MainWindow::loadCSVToTable(const QString& filename) {
    QFile file(filename);
    currentFilename = filename;
    if (!file.open(QIODevice::ReadOnly | QIODevice::Text)) {
        QMessageBox::warning(this, "Load Error", "Failed to open file for reading.");
        return;
    }

    clearTable(); // 清空表格数据

    QTextStream in(&file);
    QString headerLine = in.readLine(); // 第一行为表头
    QStringList headers = headerLine.split(",");
    tableModel->setColumnCount(headers.size());
    tableModel->setHorizontalHeaderLabels(headers);

    int row = 0;
    while (!in.atEnd()) {
        QString line = in.readLine();
        QStringList values = line.split(",");
        tableModel->insertRow(row);

        for (int col = 0; col < values.size(); ++col) {
            QStandardItem* item = new QStandardItem(values[col]);
            tableModel->setItem(row, col, item);
        }
        row++;
    }

    file.close();

}

void MainWindow::saveData()
{
    QString filename = QFileDialog::getSaveFileName(this, "Save CSV File", "", "CSV Files (*.csv);;All Files (*)");
    if (filename.isEmpty()) {
        return;
    }

    QFile file(filename);
    if (!file.open(QIODevice::WriteOnly | QIODevice::Text)) {
        QMessageBox::warning(this, "Save Error", "Failed to open file for saving.");
        return;
    }

    QTextStream out(&file);
    int rows = tableModel->rowCount();
    int columns = tableModel->columnCount();

    // 写入列标题
    for (int col = 0; col < columns; ++col) {
        out << tableModel->headerData(col, Qt::Horizontal).toString();
        if (col < columns - 1) out << ",";
    }
    out << "\n";

    // 写入数据
    for (int row = 0; row < rows; ++row) {
        for (int col = 0; col < columns; ++col) {
            out << tableModel->data(tableModel->index(row, col)).toString();
            if (col < columns - 1) out << ",";
        }
        out << "\n";
    }

    file.close();
}

// 清空表格数据
void MainWindow::clearTable() {
    tableModel->clear();
    tableModel->setRowCount(0);
    tableModel->setColumnCount(0);
}

// 预测功能（可按需修改）
void MainWindow::onPredictButtonClicked() {
    // 获取用户输入的数据
    QString input = lineEdit->text();
    if (input.isEmpty()) {
        QMessageBox::warning(this, "Input Error", "Please enter data for prediction.");
        return;
    }

    // 假设输入的是一个逗号分隔的字符串，例如 "Sunny, Hot, High, Weak"
    std::vector<std::string> inputAttributes;
    std::stringstream ss(input.toUtf8().constData());  // 使用 toUtf8() 和 constData() 转换
    std::string attribute;
    while (std::getline(ss, attribute, ',')) {
        inputAttributes.push_back(attribute);
    }

    // 检查输入的属性是否为4个
    if (inputAttributes.size() != 4) {
        QMessageBox::warning(this, "Input Error", "Please enter exactly four attributes (Weather, Temperature, Humidity, Wind).");
        return;
    }
    
    // 清理输入数据中的空格
    for (auto& attr : inputAttributes) {
        std::stringstream ss(input.toUtf8().constData());
    }
    
    // 创建测试数据
    std::map<std::string, std::string> testAttributes;
    testAttributes["Weather"] = inputAttributes[0];
    testAttributes["Temperature"] = inputAttributes[1];
    testAttributes["Humidity"] = inputAttributes[2];
    testAttributes["Wind"] = inputAttributes[3];

    

    // 加载数据集
    std::vector<Data> dataset = loadData("DT_data.csv");
    if (dataset.empty()) {
        qDebug() << "Error: No data available to build the decision tree.";
        return;
    }

    // 构建决策树
    std::set<std::string> usedAttributes;
    TreeNode* root = buildTree(dataset, usedAttributes);
    /*测试可用性*/
    /*QMessageBox::warning(this, "Input Error", "Please3");*/
    if (root == nullptr) {
        QMessageBox::warning(this, "Error", "Decision tree could not be built.");
        return;
    }

    // 使用决策树进行预测
    std::string prediction = predict(root, testAttributes);

    // 显示预测结果在 QLabel 中
    if (!prediction.empty()) {
        resultLabel->setText("Prediction result: " + QString::fromStdString(prediction));
    }
    else {
        resultLabel->setText("Prediction result: Unable to predict.");
    }
}

void MainWindow::onShowWeightsButtonClicked() {
    // 获取数据集
    std::vector<Data> dataset = loadData("DT_data.csv");

    // 计算特征权重（简单版本：只计算增益）
    auto simpleWeights = computeFeatureWeightsSimple(dataset);

    // 使用 QStringStream 来构建输出的字符串
    QString simpleOutput;
    for (const auto& feature : simpleWeights) {
        simpleOutput += QString("Feature: %1, Gain: %2\n")
            .arg(QString::fromStdString(feature.first))
            .arg(feature.second);
    }

    // 计算特征权重（详细版本：包括增益和每个取值的权重）
    auto detailedWeights = computeFeatureWeightsDetailed(dataset);

    QString detailedOutput;
    for (const auto& feature : detailedWeights) {
        detailedOutput += QString("Feature: %1, Gain: %2\n")
            .arg(QString::fromStdString(feature.first))
            .arg(feature.second.gain);
        detailedOutput += "Values:\n";
        for (const auto& value : feature.second.values) {
            detailedOutput += QString("  Value: %1, Weight: %2\n")
                .arg(QString::fromStdString(value.first))
                .arg(value.second);
        }
    }

    // 创建并显示 QMessageBox
    QMessageBox msgBox;
    msgBox.setWindowTitle("Feature Weights");

    // 输出简单的特征权重
    msgBox.setText("Simple Weights:\n" + simpleOutput);

    // 显示消息框
    msgBox.exec();

    // 输出详细的特征权重
    msgBox.setText("Detailed Weights:\n" + detailedOutput);
    msgBox.exec();
}







