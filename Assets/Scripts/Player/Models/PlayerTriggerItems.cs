using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace BBX.Player.Models
{
    public class PlayerTriggerItems
    {
        private Item.Factory _itemFactory;
        
        public List<Item> Items = new List<Item>();


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="itemFactory"></param>
        public PlayerTriggerItems(Item.Factory itemFactory)
        {
            _itemFactory = itemFactory;
        }


        /// <summary>
        /// Add an item to the triggers list
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="distance"></param>
        public void AddItem(GameObject gameObject, float distance)
        {
            var item = Items.FirstOrDefault(x => x.GameObject == gameObject);

            if (item == null)
            {
                Items.Add(_itemFactory.Create(
                    gameObject.tag,
                    distance,
                    gameObject
                ));
            }
        }


        /// <summary>
        /// Update an item's distance in the trigger list
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="distance"></param>
        public void UpdateItem(GameObject gameObject, float distance)
        {
            var item = Items.FirstOrDefault(x => x.GameObject == gameObject);
            
            if (item != null)
            {
                item.Distance = distance;
            }
        }

        
        /// <summary>
        /// Remove an item from the trigger list
        /// </summary>
        /// <param name="gameObject"></param>
        public void RemoveItem(GameObject gameObject)
        {
            var item = Items.FirstOrDefault(x => x.GameObject == gameObject);

            if (item != null)
            {
                Items.Remove(item);
            }
        }

        
        /// <summary>
        /// Check the player is in a trigger with a specified tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool ContainsTag(string tag)
        {
            return Items.Count(x => x.Tag == tag) > 0;
        }
        
        
        /// <summary>
        /// A single trigger item
        /// </summary>
        public class Item
        {
            public string Tag { get; set; }
            public float Distance { get; set; }
            public GameObject GameObject { get; set; }


            public Item(
                string tag,
                float distance,
                GameObject gameObject)
            {
                Tag = tag;
                Distance = distance;
                GameObject = gameObject;
            }
            
            public class Factory : PlaceholderFactory<string, float, GameObject, Item>
            {
            }
        }
    }
}