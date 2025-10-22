# Student Success Modeling
from dataclasses import dataclass
from typing import List, Optional
import numpy as np


@dataclass
class Student:
    academic_performance: float  # 0-100
    social_activity: float  # 0-100
    extra_curricular: bool
    friends_count: int
    has_both_parents: bool
    parents_education: List[str]
    parents_employed: List[bool]
    future_plans: str


class StudentSuccessModel:
    def __init__(self):
        self.success_threshold = 75

    def predict_success(self, student: Student) -> bool:
        # Cognitive model - weighted factors
        weights = {
            'academic': 0.4,
            'social': 0.2,
            'support': 0.2,
            'motivation': 0.2
        }

        score = (
                weights['academic'] * student.academic_performance +
                weights['social'] * (student.social_activity + student.friends_count * 5) +
                weights['support'] * self._calculate_support_score(student) +
                weights['motivation'] * self._calculate_motivation_score(student)
        )

        return score >= self.success_threshold

    def _calculate_support_score(self, student: Student) -> float:
        base_score = 60 if student.has_both_parents else 30
        education_bonus = sum(10 for edu in student.parents_education if 'higher' in edu.lower())
        employment_bonus = sum(10 for employed in student.parents_employed if employed)
        return min(100, base_score + education_bonus + employment_bonus)

    def _calculate_motivation_score(self, student: Student) -> float:
        return 100 if 'university' in student.future_plans.lower() else 50


# Epidemic Model
class EpidemicModel:
    def __init__(self, population: float, infection_rate: float, recovery_rate: float):
        self.S = population  # Susceptible
        self.I = 0  # Infected
        self.R = 0  # Recovered
        self.beta = infection_rate
        self.gamma = recovery_rate

    def step(self) -> tuple:
        """SIR model step"""
        new_infections = self.beta * self.S * self.I / (self.S + self.I + self.R)
        new_recoveries = self.gamma * self.I

        self.S -= new_infections
        self.I += new_infections - new_recoveries
        self.R += new_recoveries

        return (self.S, self.I, self.R)


# Factory Workshop Model
class WorkshopModel:
    def __init__(self, machines: int, workers: int, shift_hours: int):
        self.machines = machines
        self.workers = workers
        self.shift_hours = shift_hours
        self.efficiency = 0.85

    def calculate_daily_output(self, pieces_per_hour: float) -> float:
        return (
                self.machines *
                self.workers *
                self.shift_hours *
                pieces_per_hour *
                self.efficiency
        )


# Example Usage
def main():
    # Student Success Example
    student = Student(
        academic_performance=85,
        social_activity=70,
        extra_curricular=True,
        friends_count=3,
        has_both_parents=True,
        parents_education=['higher', 'secondary'],
        parents_employed=[True, True],
        future_plans='university'
    )

    model = StudentSuccessModel()
    success_prediction = model.predict_success(student)
    print(f"Student predicted to be successful: {success_prediction}")

    # Epidemic Example
    epidemic = EpidemicModel(population=10000, infection_rate=0.3, recovery_rate=0.1)
    for _ in range(10):
        s, i, r = epidemic.step()
        print(f"Day {_ + 1}: Susceptible={s:.0f}, Infected={i:.0f}, Recovered={r:.0f}")

    # Workshop Example
    workshop = WorkshopModel(machines=5, workers=10, shift_hours=8)
    daily_output = workshop.calculate_daily_output(pieces_per_hour=10)
    print(f"Workshop daily output: {daily_output:.0f} pieces")


if __name__ == "__main__":
    main()