import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from sklearn.preprocessing import MinMaxScaler
from tensorflow.keras.models import Sequential
from tensorflow.keras.layers import LSTM, Dense, Dropout
from tensorflow.keras.callbacks import EarlyStopping


# 数据预处理
def preprocess_data(csv_content):
    # 将CSV内容转换为DataFrame
    df = pd.read_csv(csv_content)

    # 转换日期格式并排序
    df['date'] = pd.to_datetime(df['date'], errors='coerce')
    df = df.sort_values('date').dropna(subset=['date'])

    # 使用收盘价作为主要特征
    data = df[['close']].values

    # 数据标准化
    scaler = MinMaxScaler(feature_range=(0, 1))
    scaled_data = scaler.fit_transform(data)

    return scaled_data, scaler, df


# 创建时间序列数据集
def create_dataset(data, time_step=60):
    X, y = [], []
    for i in range(len(data) - time_step - 1):
        X.append(data[i:(i + time_step), 0])
        y.append(data[i + time_step, 0])
    return np.array(X), np.array(y)


# 构建LSTM模型
def build_lstm_model(input_shape):
    model = Sequential([
        LSTM(50, return_sequences=True, input_shape=input_shape),
        Dropout(0.2),
        LSTM(50, return_sequences=False),
        Dropout(0.2),
        Dense(25),
        Dense(1)
    ])
    model.compile(optimizer='adam', loss='mean_squared_error')
    return model


# 主函数
def main(csv_content):
    # 数据预处理
    scaled_data, scaler, df = preprocess_data(csv_content)

    # 设置时间步长（使用60天历史预测下一天）
    time_step = 60
    X, y = create_dataset(scaled_data, time_step)

    # 划分训练集和测试集 (80-20)
    split = int(0.8 * len(X))
    X_train, X_test = X[:split], X[split:]
    y_train, y_test = y[:split], y[split:]

    # 重塑数据为LSTM输入格式 [样本数, 时间步长, 特征数]
    X_train = X_train.reshape(X_train.shape[0], X_train.shape[1], 1)
    X_test = X_test.reshape(X_test.shape[0], X_test.shape[1], 1)

    # 构建并训练LSTM模型
    model = build_lstm_model((X_train.shape[1], 1))

    early_stop = EarlyStopping(monitor='val_loss', patience=5)
    history = model.fit(
        X_train, y_train,
        validation_data=(X_test, y_test),
        epochs=50,
        batch_size=32,
        callbacks=[early_stop],
        verbose=1
    )

    # 进行预测
    train_predict = model.predict(X_train)
    test_predict = model.predict(X_test)

    # 反标准化
    train_predict = scaler.inverse_transform(train_predict)
    test_predict = scaler.inverse_transform(test_predict)
    y_train_actual = scaler.inverse_transform(y_train.reshape(-1, 1))
    y_test_actual = scaler.inverse_transform(y_test.reshape(-1, 1))

    # 可视化结果
    plt.figure(figsize=(16, 8))

    # 训练集预测结果
    train_plot = np.empty_like(scaled_data)
    train_plot[:, :] = np.nan
    train_plot[time_step:len(train_predict) + time_step, :] = train_predict

    # 测试集预测结果
    test_plot = np.empty_like(scaled_data)
    test_plot[:, :] = np.nan
    test_plot[len(train_predict) + (time_step * 2) + 1:len(scaled_data) - 1, :] = test_predict

    # 原始数据
    plt.plot(scaler.inverse_transform(scaled_data), label='Actual Price')
    plt.plot(train_plot, label='Training Prediction')
    plt.plot(test_plot, label='Testing Prediction')

    plt.title('Stock Price Prediction with LSTM')
    plt.xlabel('Time (Days)')
    plt.ylabel('Stock Price')
    plt.legend()
    plt.show()

    return model, history


# 使用示例
if __name__ == "__main__":
    # 假设CSV内容已读取为字符串
    # 示例调用：
    # with open('stock_data.csv', 'r') as f:
    #     csv_content = f.read()
    # model, history = main(csv_content)
    pass