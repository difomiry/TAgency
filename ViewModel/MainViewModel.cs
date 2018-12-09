﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using System.Windows.Input;

namespace TAgency.ViewModel
{
    /// <summary>
    /// The Main view model.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// The path to database.
        /// </summary>
        private string pathToDatabase = "undefined";
        
        /// <summary>
        /// The "New" command.
        /// </summary>
        private ICommand newCommand;

        /// <summary>
        /// The "Open" command.
        /// </summary>
        private ICommand openCommand;

        /// <summary>
        /// The "Save" command.
        /// </summary>
        private ICommand saveCommand;

        /// <summary>
        /// The "Exit" command.
        /// </summary>
        private ICommand exitCommand;

        /// <summary>
        /// The "About" command.
        /// </summary>
        private ICommand aboutCommand;

        #endregion

        #region Properties

        /// <summary>
        /// The path to database.
        /// </summary>
        public string PathToDatabase
        {
            get => pathToDatabase;
            set => Set(ref pathToDatabase, value);
        }

        /// <summary>
        /// The "New" command.
        /// </summary>
        public ICommand NewCommand
        {
            get => newCommand;
            set => Set(ref newCommand, value);
        }

        /// <summary>
        /// The "Open" command.
        /// </summary>
        public ICommand OpenCommand
        {
            get => openCommand;
            set => Set(ref openCommand, value);
        }

        /// <summary>
        /// The "Save" command.
        /// </summary>
        public ICommand SaveCommand
        {
            get => saveCommand;
            set => Set(ref saveCommand, value);
        }

        /// <summary>
        /// The "Exit" command.
        /// </summary>
        public ICommand ExitCommand
        {
            get => exitCommand;
            set => Set(ref exitCommand, value);
        }

        /// <summary>
        /// The "About" command.
        /// </summary>
        public ICommand AboutCommand
        {
            get => aboutCommand;
            set => Set(ref aboutCommand, value);
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            PathToDatabase = Properties.Settings.Default.PathToDatabase;

            NewCommand = new RelayCommand(NewCommandExecute);
            OpenCommand = new RelayCommand(OpenCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            ExitCommand = new RelayCommand(ExitCommandExecute);
            AboutCommand = new RelayCommand(AboutCommandExecute);
        }

        /// <summary>
        /// Opens SaveFileDialog for new database creation.
        /// </summary>
        private void NewCommandExecute()
        {
        }

        /// <summary>
        /// Opens OpenFileDialog for new database selection.
        /// </summary>
        private void OpenCommandExecute()
        {
        }

        /// <summary>
        /// Opens SaveFileDialog for current database saving.
        /// </summary>
        private void SaveCommandExecute()
        {
        }

        /// <summary>
        /// Exits the applicaton.
        /// </summary>
        private void ExitCommandExecute()
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Shows information about the application.
        /// </summary>
        private void AboutCommandExecute()
        {
            MessageBox.Show("Разработано в качестве курсового проекта по дисциплине \"Управление данными\".", "О программе");
        }
    }
}