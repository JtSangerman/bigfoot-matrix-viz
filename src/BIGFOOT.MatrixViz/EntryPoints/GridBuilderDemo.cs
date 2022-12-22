using BIGFOOT.MatrixViz.Inputs.Drivers.Controllers;
using BIGFOOT.MatrixViz.MatrixTypes.Direct2D;
using BIGFOOT.MatrixViz.Visuals.GridBuilder;
using BIGFOOT.MatrixViz.Visuals.Maze;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BIGFOOT.MatrixViz.EntryPoints
{
    internal class GridBuilderDemo
    {
        static string EX_MAZE_STR =
@"############################################################# #
# #####       #               #     #         #       #       #
# ######### # ####### ##### # # ### # ### ##### ##### # # ### #
# #   ##    #       #     # # # #   #   #     #   # #   #   # #
# # # # ########### ####### ### # ##### ##### ### # ####### ###
# # # # #####             # #   #     #     #   #   #     #   #
# ### # ################# # # ##### ### ##### ##### ### ##### #
#     #   #       #   #     # #   #     #     #     #   #     #
##### ########### # # ####### ### ####### ### # ##### # # ### #
#   #   ##  #     # #         #     #     #   # #     # #   # #
# # ######### ###   ########### ### # ######### ### ##### # ###
# #   #   #   #   #   #         #   # #             #   # #   #
# ### # # # ######### ####### # # ### # ############# # ##### #
#   #   #   #       #       # # #   #   #     #       # #   # #
### ####### # ### # ####### # # ### ##### ### # ####### # # # #
#   #     # #   # # #     # # # #     #   #   # #   #   # #   #
# ### ### ##### # ### ### # # # ##### # ### ### ### # ### #####
#     # #       #       # # # # #   # # # #     #   # #   #   #
# ##### ########### ##### # ### # # ### # ####### ### # ### # #
#   #     #       # #     # #   # #     #       #   # # # # # #
### # ##### ##### ### ### # # # # ####### ##### # # # # # # # #
#   #   #   #         #   #   # #     # # #   #   # #   #   # #
# ##### # ############# ####### ####### # ### ##### ######### #
#       #       #       # #   #     #   # #     # #           #
####### ####### # ####### # # ##### # ### # ### # ##### ##### #
#             # #   #   #   #   #   #       #       #   #     #
# ######### ### ### # # ####### # ######### ######### ### #####
# #     #   #   # #   #         #   #   #   #       # #     # #
### ### # ### ### ######### ####### # # # ### ##### # ##### # #
#   #   #   #   #           #     #   # #   # #   # #   #   # #
# ### ##### ### # ########### ### ##### ##### # # # # # # ### #
#                                                             #
#                                                             #
#                                                             #
# # ##### ### # ### ### ##### ####### # ##### # # ### ### # # #
# #   # #   # # # # #   #     #     # #     # # #   # # # #   #
# ### # ### # # # # # ### ### # ### # ##### ### ### # # # ### #
# #   #     # # #   # # #   # #   # # #     #   #   #   #   # #
# # ####### ### # ### # ### ##### # # # ##### ### ##### ### ###
# #       #     # # # #   #       # #   # #     # #       #   #
# ####### ####### # # ### ######### ##### # ### # ####### ### #
#       #     #   # # #   # #     #   #     # # #       # #   #
####### ##### # ### # # # # # ### ### # ##### # ####### ### # #
#   #   #     #     #   #   # #       # # #   #       #     # #
# # ##### ######### ####### # ### ##### # # # ####### ####### #
# #       #   #     #     # #   # #     # # # #     #     #   #
# ######### # # ##### # ### ### ### ##### # # # ### ##### # ###
#   #       #   #     # #   #   #   #   #   # # # #     # # # #
# # ### ######### ####### ### ### ### # # ### # # ### # # # # #
# #   # #     #     #     # #   # # # #     # # #   # # #   # #
# ### # ### # # ### # ##### ### # # # ####### # # # # ####### #
#   # #     # # #   #         # # # #     #   #   # #     #   #
##### # ##### ### ##### ####### # # ##### # ### ####### # ### #
#     # #   #   #     # #     # # #     # #     #       #   # #
# ####### # ### ### ### # ### # # ##### # ####### ######### # #
#         # # # #   #   # # #   #     #   #       #     #   # #
# ######### # # # # # ### # ### ##### ##### ####### ##### ### #
# #   #     #     # #   # #     #   #   #   #       #   #   # #
# ### # ### ####### ### # ####### # ### # ### ### ### # ### # #
#     #   # #   #   #   #     #   #   # # # # #   #   #   # # #
##### ### ### # ##### ####### # ### ### # # # # ### ##### # # #
#       #     #             #     #       #   #         #     #
# #############################################################

";
        static async Task Main(string[] args)
        {
            try
            {
                while (true)
                {
                    int tickMs = 3;

                    var deserializedEditedMapResult = await AsyncStartMapEditor().ConfigureAwait(false);
                    await Task.Delay(500);

                    var mazeViz = new MazeHolder<Direct2DMatrix, Direct2DCanvas>(new Direct2DMatrix(64), deserializedEditedMapResult, tickMs);
                    await Task.Delay(500);

                    await Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(mazeViz, tickMs).ConfigureAwait(false);
                    await Task.Delay(500);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static async Task<string> AsyncStartMapEditor(CancellationToken cancellationToken = default)
        {
            var matrix = new Direct2DMatrix(64);
            ControllerInputDriverBase input = new KeyboardConsoleDriver();

            var mapBuilder = new GridBuilder<Direct2DMatrix, Direct2DCanvas>(matrix, input, EX_MAZE_STR);
            await Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(mapBuilder, 50).ConfigureAwait(false);
            var finishedGrid = mapBuilder.SerializeMapState();
            Console.WriteLine("edited Grid start: ---");
            Console.WriteLine(finishedGrid);

            Console.WriteLine("--- end:");

            return finishedGrid;
        }


        public static async Task<string> AsyncGenerateMazeString(CancellationToken cancellationToken = default)
        {
            var matrix = new Direct2DMatrix(64);
            var maze = new MazeHolder<Direct2DMatrix, Direct2DCanvas>(matrix, 1);
            await Direct2DVisualEngine.BeginVirtualDirect2DGraphicsVisualEmulation(maze, 1).ConfigureAwait(false);
            var mazeStr = maze.SerializedMazeStr;
            Console.WriteLine("maze start: ---");
            Console.WriteLine(mazeStr);
            Console.WriteLine("--- end:");

            return mazeStr;
        }
    }
}
