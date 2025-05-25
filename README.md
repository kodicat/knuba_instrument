# DiscreteSolver

**DiscreteSolver** — це настільний застосунок на C# для автоматичного розв’язання типових задач з дискретної математики. Ідеально підходить для студентів та викладачів, які хочуть швидко перевірити рішення, побудувати таблиці істинності або попрацювати з множинами та графами.

---

## 🔧 Основні можливості

- 🔢 **Булева алгебра**
    - Обчислення значень логічних виразів
    - Побудова таблиць істинності
    - Спрощення логічних виразів (у розробці)

- 📚 **Теорія множин**
    - Операції: ∪, ∩, \, ⊕
    - Побудова діаграм Венна (у розробці)

- 🎲 **Комбінаторика**
    - Перестановки, розміщення, комбінації
    - Біноміальні коефіцієнти

- 🕸️ **Графи**
    - Побудова графів за матрицею суміжності
    - Перевірка зв’язності
    - Обчислення степенів вершин

---

## 🔧 Як запустити проєкт

1. Переконайтесь, що у вас встановлено [.NET 8 SDK](https://dotnet.microsoft.com/).
1. Склонуйте репозиторій:
   ```bash
   git clone git@github.com:kodicat/knuba_instrument.git
   cd knuba_instrument
   ```
1. Запустіть консольний застосунок з командного рядка:
   ```bash
   cd cd DiscreteSolver.Console
   dotnet run
   ```
1. Введіть вираз теорії множин до прикладу - `(A' + C)' + (B + B * C) * (B' + (B + C)')`

---

## 🔧 Як тестувати проект

1. Переконайтесь, що у вас встановлено [.NET 8 SDK](https://dotnet.microsoft.com/).
1. Запуст тестів:
   ```bash
   dotnet test
   ```
1. Запустіть тести з розширеним виводом:
   ```bash
   dotnet test --logger:"console;verbosity=detailed"
   ```
1. Переконайтесь, що у вас встановлено coverlet.console:
   ```bash
   dotnet tool install --global coverlet.console
   ```
1. Переконайтесь, що у вас встановлено dotnet-reportgenerator-globaltool:
   ```bash
   dotnet tool install --global dotnet-reportgenerator-globaltool
   ```
1. Перейдіть в директорію з тестовим проектом:
   ```
   cd DiscreteSolver.Tests
   ```
1. Запустіть покриття тестів:
   ```bash
   coverlet ./bin/Debug/net8.0/DiscreteSolver.Tests.dll \
    --target "dotnet" \
    --targetargs "test --no-build" \
    --format cobertura \
    --output ./TestResults/coverage.cobertura.xml
   ```
1. Згенеруйте HTML-звіт з coverage.cobertura.xml:
   ```bash
   reportgenerator \
    -reports:./TestResults/coverage.cobertura.xml \
    -targetdir:./TestResults/coverage-report \
    -reporttypes:Html
   ```
1. Відкрити HTML-звіт
   ```bash
   open ./TestResults/coverage-report/index.html
   ```