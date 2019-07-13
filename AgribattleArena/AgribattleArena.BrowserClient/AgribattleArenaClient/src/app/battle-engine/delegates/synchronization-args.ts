import { BattleScene } from '../battle-scene';
import { BattleActor, BattleDecoration, BattleSpecEffect, BattleTile } from '../objects';
import { ISyncActor, ISyncDecoration, ISyncSpecEffect, ISyncTile } from 'src/app/share/models/synchronization';
import { Type, TypeofExpr } from '@angular/compiler';

export type OnRemoveArg = (scene: BattleScene, index: number,
                           type: typeof BattleActor | typeof BattleDecoration | typeof BattleSpecEffect) => void;

export function killActorDefault(scene: BattleScene, index: number,
                                 type: typeof BattleActor | typeof BattleDecoration | typeof BattleSpecEffect) {
    if (type === BattleActor) {
        scene.actors.splice(index, 1);
    }
}

export function killDecorationDefault(scene: BattleScene, index: number,
                                      type: typeof BattleActor | typeof BattleDecoration | typeof BattleSpecEffect) {
    if (type === BattleDecoration) {
        scene.decorations.splice(index, 1);
    }
}

export function killSpecEffectDefault(scene: BattleScene, index: number,
                                      type: typeof BattleActor | typeof BattleDecoration | typeof BattleSpecEffect) {
    if (type === BattleSpecEffect) {
        scene.specEffects.splice(index, 1);
    }
}

export type OnChangeArg = (scene: BattleScene, element: BattleActor | BattleDecoration | BattleSpecEffect | BattleTile,
                           result: ISyncActor | ISyncDecoration | ISyncSpecEffect | ISyncTile) => void;

export function changeActorDefault(scene: BattleScene, element: BattleActor | BattleDecoration | BattleSpecEffect | BattleTile,
                                   result: ISyncActor | ISyncDecoration | ISyncSpecEffect | ISyncTile) {
    if (element instanceof BattleActor) {

    }
}

export function changeDecorationDefault(scene: BattleScene, element: BattleActor | BattleDecoration | BattleSpecEffect | BattleTile,
                                        result: ISyncActor | ISyncDecoration | ISyncSpecEffect | ISyncTile) {
    if (element instanceof BattleDecoration) {

    }
}

export function changeSpecEffectDefault(scene: BattleScene, element: BattleActor | BattleDecoration | BattleSpecEffect | BattleTile,
                                        result: ISyncActor | ISyncDecoration | ISyncSpecEffect | ISyncTile) {
    if (element instanceof BattleSpecEffect) {

    }
}

export function changeTileDefault(scene: BattleScene, element: BattleActor | BattleDecoration | BattleSpecEffect | BattleTile,
                                  result: ISyncActor | ISyncDecoration | ISyncSpecEffect | ISyncTile) {
    if (element instanceof BattleTile) {

    }
}

export type OnCreateArg = (scene: BattleScene, element: ISyncActor | ISyncDecoration | ISyncSpecEffect,
                           type: typeof BattleDecoration | typeof BattleActor | typeof BattleSpecEffect) => void;

export function newActorDefault(scene: BattleScene, element: ISyncActor | ISyncDecoration | ISyncSpecEffect,
                                type: typeof BattleDecoration | typeof BattleActor | typeof BattleSpecEffect) {
    if (type === BattleActor) {

    }
}

export function newDecorationDefault(scene: BattleScene, element: ISyncActor | ISyncDecoration | ISyncSpecEffect,
                                     type: typeof BattleDecoration | typeof BattleActor | typeof BattleSpecEffect) {
    if (type === BattleDecoration) {

    }
}


export function newSpecEffectDefault(scene: BattleScene, element: ISyncActor | ISyncDecoration | ISyncSpecEffect,
                                     type: typeof BattleDecoration | typeof BattleActor | typeof BattleSpecEffect) {
    if (type === BattleSpecEffect) {

    }
}

