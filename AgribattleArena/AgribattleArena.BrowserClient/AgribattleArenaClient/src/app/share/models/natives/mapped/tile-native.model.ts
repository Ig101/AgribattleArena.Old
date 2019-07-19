import { TileAction, TileOnStepAction } from 'src/app/battle-engine/delegates/tile-delegates';

export interface ITileNative {
    id: string;
    sprite: string;
    action: TileAction;
    onStepAction: TileOnStepAction;

    name: string;
    description: string;
}
