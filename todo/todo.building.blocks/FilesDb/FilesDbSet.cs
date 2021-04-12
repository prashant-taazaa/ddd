using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using todo.domain;

namespace todo.infrastructure.shared.FilesDb
{
    public  class FilesDbSet<TEntity>  where TEntity : Aggregate
    {
        private string _filePath;

        public FilesDbSet(string path)
        {
            _filePath = Path.Combine(path, typeof(TEntity).Name + ".json");
        }

        public  void Add(TEntity entity)
        {
            var data = Read().Append(entity);
            Save(data);
        }

        public virtual async System.Threading.Tasks.Task AddAsync(TEntity entity)
        {
            var data = Read().Append(entity);
            await SaveAsync(data);
        }


        public  IEnumerable<TEntity> Get()
        {
            return Read();       
        }

        public TEntity GetByIdAsync(Guid id)
        {
            //return Read().Where(x => x.Id == id)
            //    .FirstOrDefault();
            throw new Exception();
        }

        public TEntity GetById(Guid id)
        {
            var data = Read();
            return data
                .FirstOrDefault(x => x.Id == id);
        }

        public  IAsyncEnumerable<TEntity> GetAsync()
        {
            return ReadAsync();
        }


        public  void Remove(TEntity entity)
        {
            var data = Read().Where(x => x.Id != entity.Id);
            Save(data);
        }

        public  async System.Threading.Tasks.Task RemoveAsync(TEntity entity)
        {
            var data = Read().Where(x => x.Id != entity.Id);
            await SaveAsync(data);
        }

        private IEnumerable<TEntity>Read()
        {
            var serializer = new JsonSerializer();
            TEntity entity;

            if (File.Exists(_filePath))
            {
                using FileStream fileStream = File.Open(_filePath, FileMode.Open, FileAccess.Read);
                using StreamReader streamReader = new StreamReader(fileStream);
                using (JsonReader reader = new JsonTextReader(streamReader))
                {
                    while (reader.Read())
                    {
                        // deserialize only when there's "{" character in the stream
                        if (reader.TokenType == JsonToken.StartObject)
                        {
                            entity = serializer.Deserialize<TEntity>(reader);

                            yield return entity;
                        }
                    }
                }
            }
            else
            {
                Save(new List<TEntity>());
            }
        }

        private async IAsyncEnumerable<TEntity> ReadAsync()
        {
            var serializer = new Newtonsoft.Json.JsonSerializer();
            TEntity entity;

            if (File.Exists(_filePath))
            {
                using FileStream fileStream = File.Open(_filePath, FileMode.Open);
                using StreamReader streamReader = new StreamReader(fileStream);
                using JsonReader reader = new JsonTextReader(streamReader);

                while (await reader.ReadAsync())
                {
                    // deserialize only when there's "{" character in the stream
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        entity = serializer.Deserialize<TEntity>(reader);

                        yield return entity;
                    }
                }
            }
        }

        private async System.Threading.Tasks.Task SaveAsync(IEnumerable<TEntity> entities)
        {
           await File.WriteAllTextAsync(_filePath, JsonConvert.SerializeObject(entities));
        }
        private void Save(IEnumerable<TEntity> entities)
        {
             File.WriteAllText(_filePath, JsonConvert.SerializeObject(entities));
        }


    }
}
