# BubblyChat

**BubblyChat** is a modern, responsive desktop messaging application developed using **C#** and **WPF**, following the **MVVM (Model-View-ViewModel)** architectural pattern. The project aims to provide a clean, scalable structure with an elegant user interface and features such as user authentication, a chat interface, and support for custom WPF controls.

---

## ✨ Features

* **MVVM Architecture** – Ensures clean separation of concerns for better maintainability.
* **User Authentication UI** – Login and registration screens designed (logic integration in progress).
* **Chat Interface** – Real-time chat layout with contacts, message history, and sending functionality.
* **Custom Controls** – Includes a `BindablePasswordBox` for secure password entry via data binding.
* **Modern UI Design** – Built using `MahApps.Metro.IconPacks` and tailored styles for a polished user experience.

---

## 🗂️ Project Structure

```
BubblyChat/
│
├── MVVM/
│   ├── Model/         # Data models: User, Contact, Message
│   ├── View/          # XAML Views: Login, Register, Main, Chat
│   └── ViewModel/     # ViewModels: Handles logic and data binding
│
├── Core/              # Base classes (RelayCommand, ViewModelBase)
├── CustomControls/    # Custom WPF controls (e.g., BindablePasswordBox)
├── Styles/            # XAML Resource Dictionaries for UI styling
└── Images/            # Static image assets
```

---

## ⚙️ Getting Started

### Prerequisites

* Windows 10 or later
* Visual Studio 2019 or newer
* .NET Framework 4.7.2

### Setup Instructions

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/BubblyChat.git
   ```
2. Open `BubblyChat.sln` in Visual Studio.
3. Restore NuGet packages (Right-click the solution > **Restore NuGet Packages**).
4. Build and run the project.

---

## 📦 Dependencies

* [MahApps.Metro.IconPacks](https://github.com/MahApps/MahApps.Metro.IconPacks)
* [FirebaseAuthentication.net](https://github.com/step-up-labs/firebase-authentication-dotnet) *(planned for integration)*
* `Microsoft.Extensions.*` packages for configuration, dependency injection, etc.

---

## 🧠 Architecture Overview

* **Navigation**: Handled by `NavigationViewModel` to switch between screens.
* **Chat System**: `ChattingViewModel` manages message lists and selected contact conversations.
* **Authentication**: Login and register UI ready; business logic is scaffolded for future integration.
* **Custom Controls**: Secure password entry through MVVM-friendly `BindablePasswordBox`.

---

## 📘 Developer Learning Guide

### 📌 Required Knowledge

#### 1. C# & .NET Framework

* Object-oriented programming, events, and delegates
* ObservableCollection, data binding, and command patterns

#### 2. WPF (Windows Presentation Foundation)

* XAML layout, styles, templates, and resource dictionaries
* User controls and data templates

#### 3. MVVM Pattern

* Separation of concerns: Model, View, ViewModel
* `INotifyPropertyChanged`, `ICommand`, and `RelayCommand`

#### 4. Project & Dependency Management

* NuGet for third-party packages
* Visual Studio project configuration and references

#### 5. (Optional/Future Scope)

* Firebase authentication and secure credential handling
* Async programming for chat/network interactions

---

## 📚 Theory Highlights

* MVVM in practice
* Data binding modes (one-way, two-way, command binding)
* ObservableCollection vs. List<T> in UI updates
* Dependency Injection basics
* Creating and using custom controls in WPF
* Asynchronous programming patterns in C#

---

## 👥 Team

BubblyChat is developed and maintained by the following team members:

* **Anh Tuan** – Project Lead / UI/UX Designer / WPF Developer
* **Tran Dong Truc Lam** – WPF Developer
* **Ngo Thai Vinh** – WPF Developer


> Want to join or contribute? See below.

---

## 🤝 Contributing

Contributions are welcome! If you want to propose new features or improvements, feel free to:

1. Fork the repository.
2. Create a new branch.
3. Make your changes and submit a Pull Request.
4. For major changes, please open an issue first for discussion.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
