import { BattleScene } from './battle-scene';
import { BattleChangeInstructionAction } from 'src/app/share/models/enums/battle-change-instruction-action.enum';
import { BattleActor, BattleSkill, BattleDecoration } from './objects';
import { IEventActionSignature } from './event-models/event-action-signature.model';
import { ISynchronizer } from 'src/app/share/models';
import { IEventChangeToken } from './event-models/event-change-token';

export class BattleEvent {
    actionSignature: IEventActionSignature;
    result: ISynchronizer;

    tokens: IEventChangeToken[];

    constructor(private scene: BattleScene, signature?: IEventActionSignature) {
        if (signature) {
            this.uploadSignature(signature);
        }
    }

    getSignatureFromSynchronizer(sync: ISynchronizer, changeAction: BattleChangeInstructionAction): IEventActionSignature {
        let changeActor: BattleActor | BattleDecoration = null;
        if (sync.actorId) {
            if (changeAction === BattleChangeInstructionAction.Decoration) {
                changeActor = this.scene.decorations.find(x => x.id === sync.actorId);
            } else {
                changeActor = this.scene.actors.find(x => x.id === sync.actorId);
            }
        }
        let changeSkill: BattleSkill = null;
        if (sync.skillActionId) {
            changeSkill = (changeActor as BattleActor).skills.find(x => x.id === sync.skillActionId);
        }
        return {
            action: changeAction,
            actor: changeActor,
            x: sync.targetX,
            y: sync.targetY,
            skill: changeSkill
        };
    }

    uploadSignature(signature: IEventActionSignature) {
        this.actionSignature = signature;
    }

    uploadSynchronizer(sync: ISynchronizer, action: BattleChangeInstructionAction) {
        if (!this.actionSignature) {
            this.uploadSignature(this.getSignatureFromSynchronizer(sync, action));
        }
    }

    pushToken(token: IEventChangeToken): boolean {
        this.tokens.push(token);
        return this.processTokens();
    }

    private processToken(token: IEventChangeToken) {
        // TODO
    }

    processTokens(): boolean {
        if (this.result) {
            while (this.tokens.length > 0) {
                this.processToken(this.tokens.shift());
            }
            return true;
        }
        return false;
    }
}

