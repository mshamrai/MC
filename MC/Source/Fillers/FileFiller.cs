﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using MC.Source.Graphics;
using MC.Source.Entries;
using Directory = MC.Source.Entries.Directory;
using File = MC.Source.Entries.File;
using System.Collections.Generic;

namespace MC.Source.Fillers
{
    class FileFiller
    {

        public FileFiller(GraphicalApp graphicalApp, FileSystemWatcher systemWatcher)
        {
            this.graphicalApp = graphicalApp;
            this.systemWatcher = systemWatcher;
        }

        private readonly GraphicalApp graphicalApp;
        private FileSystemWatcher systemWatcher;

        public void OpenEntry(Entity entity)
        {
            try
            {
                if (entity.IsArchive())
                {
                    var baseEntity = new List<Entity>();
                    baseEntity.Add(new Folder(Path.GetDirectoryName(entity.Path)) { Name = "...", Date = "", Size = "" });
                    graphicalApp.DataSource = new ObservableCollection<Entity>(new Zip(entity.Path).GetEntity(baseEntity));
                }
                else
                if (entity is Directory)
                {
                    var dir = entity as Directory;
                    //systemWatcher.Path = dir.Path;
                    //systemWatcher.EnableRaisingEvents = true;
                    //start fill            

                    graphicalApp.SetCaptionOfPath(dir.Path);
                    var dataList = dir.GetEntry();
                    graphicalApp.DataSource = new ObservableCollection<Entity>(dataList);
                }
                else
                {
                    (entity as File)?.Open();
                }
            }
            catch (UnauthorizedAccessException e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                var dataList = (entity as Directory).CreateDataList();
                graphicalApp.DataSource = new ObservableCollection<Entity>(dataList);
            }
        }


    }
}