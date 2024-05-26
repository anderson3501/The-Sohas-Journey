using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        
        RoomController.instance.LoadRoom("Start", 0, 0);
        foreach(Vector2Int roomLocation in rooms)
        {
    
            RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoomName(), roomLocation.x, roomLocation.y);
            
            // if (rooms != null)
            // {
            // // Asegúrate de que el ItemSpawner esté activado en la sala antes de llamar a SpawnItems
            //     if (rooms.itemSpawner != null)
            //     {
            //         rooms.itemSpawner.enabled = true;
            //         rooms.itemSpawner.SpawnItem();
            //     }
            // }
        }
    }
}
