#include "decisiontree.h"
#include <iostream>
#include <fstream>
#include <sstream>
#include <cmath>
#include <set>
#include<qdebug.h>
#include<qstring.h>
#include<qmainwindow.h>

// 加载数据集
std::vector<Data> loadData(const std::string& filename) {
    std::ifstream file(filename);
    if (!file.is_open()) {
        qDebug() << "Error: Unable to open file.";
        return {};
    }

    std::vector<Data> dataset;
    std::string line;
    bool isFirstLine = true;

    while (std::getline(file, line)) {
        std::stringstream ss(line);
        std::string value;
        std::vector<std::string> row;

        while (std::getline(ss, value, ',')) {  // CSV 文件使用,作为分隔符
            row.push_back(value);
        }

        if (isFirstLine) {
            // 跳过标题行
            isFirstLine = false;
            continue;
        }

        if (row.size() != 5) {
            qDebug() << "Invalid row detected:" << QString::fromStdString(line);
            continue;
        }

        Data data;
        data.attributes["Weather"] = row[0];
        data.attributes["Temperature"] = row[1];
        data.attributes["Humidity"] = row[2];
        data.attributes["Wind"] = row[3];
        data.label = row[4];  // "yes" 或 "no"
        dataset.push_back(data);
    }

    return dataset;
}



// 计算数据集的熵
double entropy(const std::vector<Data>& dataset) {
    std::map<std::string, int> labelCounts;
    for (const auto& data : dataset) {
        ++labelCounts[data.label];
    }

    double entropy = 0.0;
    for (const auto& count : labelCounts) {
        double prob = static_cast<double>(count.second) / dataset.size();
        entropy -= prob * log2(prob);
    }

    return entropy;
}

// 计算信息增益
double informationGain(const std::vector<Data>& dataset, const std::string& attribute) {
    // 获取该属性的所有唯一值
    std::map<std::string, std::vector<Data>> subsets;
    for (const auto& data : dataset) {
        subsets[data.attributes.at(attribute)].push_back(data);
    }

    // 计算分裂后的加权熵
    double weightedEntropy = 0.0;
    for (const auto& subset : subsets) {
        double subsetEntropy = entropy(subset.second);
        weightedEntropy += (static_cast<double>(subset.second.size()) / dataset.size()) * subsetEntropy;
    }

    // 计算信息增益
    double datasetEntropy = entropy(dataset);
    return datasetEntropy - weightedEntropy;
}

// 构建决策树
TreeNode* buildTree(std::vector<Data> dataset, std::set<std::string> usedAttributes) {
    if (dataset.empty()) {
        return nullptr;
    }

    // 计算当前数据集的标签种类
    std::map<std::string, int> labelCounts;
    for (const auto& data : dataset) {
        ++labelCounts[data.label];
    }

    // 如果所有标签都相同，返回叶子节点
    if (labelCounts.size() == 1) {
        TreeNode* leafNode = new TreeNode();
        leafNode->isLeaf = true;
        leafNode->label = dataset[0].label;
        return leafNode;
    }

    // 如果没有更多属性可用，则返回叶子节点
    if (usedAttributes.size() == dataset[0].attributes.size()) {
        TreeNode* leafNode = new TreeNode();
        leafNode->isLeaf = true;
        leafNode->label = labelCounts.begin()->first; // 最常见的标签
        return leafNode;
    }

    // 选择信息增益最大的属性
    double bestGain = -1.0;
    std::string bestAttribute;
    for (const auto& attribute : dataset[0].attributes) {
        if (usedAttributes.find(attribute.first) == usedAttributes.end()) {
            double gain = informationGain(dataset, attribute.first);
            if (gain > bestGain) {
                bestGain = gain;
                bestAttribute = attribute.first;
            }
        }
    }

    if (bestAttribute.empty()) {
        // 如果没有找到合适的分裂属性，返回叶子节点
        TreeNode* leafNode = new TreeNode();
        leafNode->isLeaf = true;
        leafNode->label = labelCounts.begin()->first; // 最常见的标签
        return leafNode;
    }

    // 创建新的决策树节点
    TreeNode* node = new TreeNode();
    node->attribute = bestAttribute;
    node->isLeaf = false;

    // 将当前属性标记为已使用
    usedAttributes.insert(bestAttribute);

    // 为每个子集递归构建子树
    std::map<std::string, std::vector<Data>> subsets;
    for (const auto& data : dataset) {
        subsets[data.attributes.at(bestAttribute)].push_back(data);
    }

    for (const auto& subset : subsets) {
        node->children[subset.first] = buildTree(subset.second, usedAttributes);
    }

    return node;
}

// 进行预测
std::string predict(TreeNode* root, const std::map<std::string, std::string>& testAttributes) {
    if (root == nullptr) {
        return "";  // 如果没有树则无法预测
    }

    TreeNode* currentNode = root;
    while (!currentNode->isLeaf) {
        std::string attributeValue = testAttributes.at(currentNode->attribute);
        currentNode = currentNode->children.at(attributeValue);  // 找到相应的子节点
    }

    qDebug() << "Prediction result: " << QString::fromStdString(currentNode->label);  // 打印预测结果
    return currentNode->label;  // 叶子节点的标签即为预测结果
}

// 计算每个特征的增益
std::map<std::string, double> computeFeatureWeightsSimple(const std::vector<Data>& dataset) {
    std::map<std::string, double> featureWeights;

    // 获取数据集中的所有特征
    for (const auto& attribute : dataset[0].attributes) {
        std::string feature = attribute.first;

        // 计算信息增益
        double gain = informationGain(dataset, feature);

        // 存储每个特征的增益
        featureWeights[feature] = gain;
    }

    return featureWeights;
}

// 计算每个特征的详细权重，包括每个取值的权重


std::map<std::string, FeatureWeight> computeFeatureWeightsDetailed(const std::vector<Data>& dataset) {
    std::map<std::string, FeatureWeight> featureWeights;

    // 获取数据集中的所有特征
    for (const auto& attribute : dataset[0].attributes) {
        std::string feature = attribute.first;

        // 计算信息增益
        double gain = informationGain(dataset, feature);

        // 计算每个取值的权重
        std::map<std::string, std::vector<Data>> subsets;
        for (const auto& data : dataset) {
            subsets[data.attributes.at(feature)].push_back(data);
        }

        // 计算每个取值的权重
        std::map<std::string, double> valueWeights;
        for (const auto& subset : subsets) {
            double subsetEntropy = entropy(subset.second);
            valueWeights[subset.first] = subsetEntropy;
        }

        // 存储计算的结果
        featureWeights[feature].gain = gain;  // 存储增益
        featureWeights[feature].values = valueWeights;  // 存储每个特征取值的权重
    }

    return featureWeights;
}







