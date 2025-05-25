using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using TiledSharp;



namespace nd_trabalho.src
{
    internal class TilemapManager
    {
        Texture2D tileset;
        TmxMap map;
        int tilesetTilesWide;
        //int tilesetTilesHeight;
        int tileWidth;
        int tileHeight;
        int camada;

        public TilemapManager(TmxMap _map, Texture2D _tileset, int _tilesetTilesWide,int _tilesetTilesHeigth ,int _tileWidth, int _tileHeight, int _camada)
        {
            map = _map;
            tileset = _tileset;
            tilesetTilesWide = _tilesetTilesWide;
            //tilesetTilesHeight = _tilesetTilesHeigth;
            tileWidth = _tileWidth;
            tileHeight = _tileHeight;
            camada = _camada;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

                for(var j = 0;j < map.Layers[camada].Tiles.Count;j++)
                {
                    int gid = map.Layers[camada].Tiles[j].Gid;
                    if (gid == 0)
                    {
                        //N faz nada
                    }
                    else 
                    {
                  
                        int tileFrame = gid - 1;
                    if (camada == 1)
                    {
                        int firstGidProps = 727;
                        tileFrame = gid - firstGidProps;
                    }
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                        float x = (j % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(j/(double)map.Width) * map.TileHeight;

                        Rectangle tilesetRec = new Rectangle((tileWidth) * column, (tileHeight) * row, tileWidth, tileHeight);
                        spriteBatch.Draw(tileset,new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    }
                }
   

        }
    }

}
