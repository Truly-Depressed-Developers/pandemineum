using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratorTools : MonoBehaviour
{
    public Tilemap GroundTilemap;
    public Tile ground_tile;
    public System.Random level_gen = null;

    public void GenerateSphere(int radious , Vector2Int sph_pos)
    {
        Vector2Int new_pos = new Vector2Int(0, 0);
        for (int x = -radious; x < radious; x++)
        {
            for (int y = -radious; y < radious; y++)
            {
                new_pos = new Vector2Int(x + sph_pos.x, y + sph_pos.y);
                if (Vector2Int.Distance(sph_pos, new_pos) < radious - 0.5f)
                    GroundTilemap.SetTile(new Vector3Int(new_pos.x, new_pos.y, 0), ground_tile);
            }
        }
    }
 
    public List<Vector3Int> GenerateSnake(int length, Vector2Int pos, float dir_angle, bool sharp_turns = true, 
        int min_size = 4 , int max_size = 7)
    {
        int start_len = length;
        // returns (x_pox , y_pox, angle)
        List<Vector3Int> joints = new List<Vector3Int>(0);
        check_gen();
        Vector2 new_pos; int rad; int joint_cap = 0;
        while (length > 0)
        {
            // set new sphere
            rad = level_gen.Next(min_size, max_size);
            GenerateSphere(rad, pos);
            new_pos = new Vector3(pos.x , pos.y , 0) + Quaternion.Euler(0,0,dir_angle) * Vector3.up * rad;
            pos = new Vector2Int((int)new_pos.x, (int)new_pos.y);

            // set joint every 3 or at the end
            if (length == 1 || joint_cap == 0)
            {
                joints.Add(new Vector3Int(pos.x, pos.y, (int)dir_angle));
                if(length > start_len/2)
                    joint_cap = 10;
            }
            else
                joint_cap--;

            // change angle
            if (level_gen.Next(0, 10) < 7 || !sharp_turns)
                dir_angle = dir_angle + level_gen.Next(-1, 2) * 12f;
            else
            {
                if (level_gen.Next(0, 2) == 1)
                    dir_angle = dir_angle + 50f;
                else
                    dir_angle = dir_angle - 50f;
            }
                
            dir_angle = correct_angle(dir_angle);
            length--;
        }
        return joints;
    }

    private void check_gen()
    {
        if(level_gen == null)
        {
            level_gen = new System.Random(generate_seed().GetHashCode());
        }
    }

    public string generate_seed()
    {
        string seed = "";
        for(int i = 0; i < 16; i++)
        {
            switch(Random.Range(0,3))
            {
                case 0:
                    seed += Random.Range('a', 'z');
                    break;
                case 1:
                    seed += Random.Range('A', 'Z');
                    break;
                case 2:
                    seed += Random.Range('0', '9');
                    break;
            }
        }
        return seed;
    }

    private float correct_angle(float angle)
    {
        while(angle > 360)
            angle -= 360;
        while (angle < 0)
            angle += 360;
        return angle;
    }
}
