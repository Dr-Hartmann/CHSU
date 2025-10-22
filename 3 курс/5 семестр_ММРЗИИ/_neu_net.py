import numpy as np

learning_rate = 0.01
max_iteration = 1000

# # #

# метод градиентного спуска: один вход - несколько выходов
def gradient_descent_one_in_many(input, weights: list, goal_pred: list, max_iteration: int):

    def neural_network(input, weights):
        return np.dot(weights, input)

    for iteration in range(max_iteration):
        prediction = neural_network(input, weights)

        error = np.zeros_like(goal_pred)
        delta = np.zeros_like(goal_pred)
        weight_delta = np.zeros_like(goal_pred)

        for i in range(len(goal_pred)):
            delta[i] = prediction[i] - goal_pred[i]
            error[i] = delta[i] ** 2
            weight_delta[i] = input * delta[i]

        print(f"Iteration: {iteration}\n"
              f"Error: {error}\n"
              f"Prediction: {prediction}\n"
              f"Goal prediction: {goal_pred}\n"
              f"Input: {input}\n"
              f"Weight: {weights}\n"
              f"Weight-delta:\n{weight_delta}\n")

        for i in range(len(weights)):
            weights[i] -= weight_delta[i] * learning_rate

        total_error = 0
        for i in range(len(error)):
            total_error += error[i]

        if(total_error <= 0.0001*len(error)):
            break

# метод градиентного спуска: несколько входов - один выход
def gradient_descent_many_in_one(input: list, weights: list, goal_pred, max_iteration: int):

    def neural_network(input, weights):
        return sum(input[j] * weights[j] for j in range(len(weights)))

    for iteration in range(max_iteration):
        prediction = neural_network(input, weights)
        delta = prediction - goal_pred
        error = delta ** 2

        weight_delta = np.zeros_like(weights)
        for i in range(len(weight_delta)):
            weight_delta[i] = input[i] * learning_rate

        print(f"Iteration: {iteration}\n"
              f"Error: {error}\n"
              f"Prediction: {prediction}\n"
              f"Goal prediction: {goal_pred}\n"
              f"Input: {input}\n"
              f"Weight: {weights}\n"
              f"Weight-delta:\n{weight_delta}\n")

        for i in range(len(weights)):
            weights[i] -= weight_delta[i] * learning_rate

        if(error <= 0.0001):
            break

# метод градиентного спуска: несколько входов - несколько выходов
def gradient_descent_many_in_many(input: list, weights: list, goal_pred: list, max_iteration: int):
    def neural_network(input, weights):
        return np.dot(weights, input)

    for iteration in range(max_iteration):
        prediction = neural_network(input, weights)
        error = np.zeros_like(goal_pred)
        delta = np.zeros_like(goal_pred)
        weight_delta = np.zeros_like(weights)

        for i in range(len(goal_pred)):
            delta[i] = prediction[i] - goal_pred[i]
            error[i] = delta[i] ** 2
            for j in range(len(delta)):
                weight_delta[i][j] = input[j] * delta[i]

        print(f"Iteration: {iteration}\n"
              f"Error: {error}\n"
              f"Prediction: {prediction}\n"
              f"Goal prediction: {goal_pred}\n"
              f"Input: {input}\n"
              f"Weight: {weights}\n"
              f"Weight-delta:\n{weight_delta}\n")
        
        for i in range(len(weights)):
            for j in range(len(weights[0])):
                weights[i][j] -= weight_delta[i][j] * learning_rate

        total_error = 0
        for i in range(len(error)):
            total_error += error[i]

        if(total_error <= 0.0001*len(error)):
            break

# светофор с нулевой ошибкой !!!!!!!!!!!!! TODO
def traffic_lights(input: list, goal_pred: list, weight_1_2: list, weight_2_3: list, max_iteration: int):
    for _ in range(max_iteration):
        for i in range(len(input)):
            layer_2 = np.dot(input[i], weight_1_2)
            prediction = np.dot(layer_2, weight_2_3)

            error = (prediction[0] - goal_pred[i]) ** 2
            layer_3_delta = prediction[0] - goal_pred[i]
            layer_2_delta = np.dot(layer_3_delta, weight_2_3)

            weight_delta_1_2 = np.zeros_like(weight_1_2)
            weight_delta_2_3 = np.zeros_like(weight_2_3)

            for k in range(len(weight_delta_1_2)):
                for j in range(len(weight_delta_1_2[k])):
                    weight_delta_1_2[k][j] = input[i][k] * layer_2_delta[k]

            for k in range(len(weight_delta_2_3)):
                for j in range(len(weight_delta_2_3[k])):
                    weight_delta_2_3[k][j] = layer_2[j] * layer_3_delta

            for k in range(len(weight_1_2)):
                for j in range(len(weight_1_2[k])):
                    weight_1_2[k][j] -= weight_delta_1_2[k][j] * learning_rate

            for j in range(len(weight_delta_2_3[k])):
                weight_2_3[j] -= weight_delta_2_3[j] * learning_rate

        print(error)

# # #

phrases = {
    'many_in_one': gradient_descent_many_in_one,
    'one_in_many': gradient_descent_one_in_many,
    'many_in_many': gradient_descent_many_in_many,
}

def gradient_descent(what, input, weights, goal_pred, max_iteration):
    phrases[what](input, weights, goal_pred, max_iteration)

# # #

# gradient_descent('many_in_one', np.array([8.4, 0.24, 1.1]), np.array([0.1, 0.49, 0.9]), 1.5, max_iteration)
# gradient_descent('one_in_many', 1.95, np.array([0.1, 0.49, 0.9]), np.array([8.4, 0.24, 1.1]), max_iteration)
gradient_descent('many_in_many', np.array([8.4, 0.24, 1.1]), np.array([[1.0, 1.0, 1.0], [1.0, 1.0, 1.0], [1.0, 1.0, 1.0]]), np.array([10.0, 5.0, 1.0]), max_iteration)

# input = np.array([[1, 1, 0],
#                   [0, 1, 0],
#                   [1, 0, 1],
#                   [0, 0, 1],
#                   [1, 1, 1]])

# weight_1_2 = np.ones((3,4))
# weight_2_3 = np.ones((4,1))

# goal_pred = np.array([1, 1, 0, 0, 1])

# traffic_lights(input, goal_pred, weight_1_2, weight_2_3, max_iteration)