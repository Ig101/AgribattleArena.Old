import { ITagSynergy } from 'src/app/share/models/tag-synergy.model';
import { BattleSkill, BattleBuff } from '.';
import { IActorNative } from 'src/app/share/models/natives/mapped';
import { BattleScene } from '../battle-scene';
import { ISyncActor } from 'src/app/share/models/synchronization';
import { BattleTile } from './battle-tile';
import { BattleChangeInstructionAction } from 'src/app/share/models/enums/battle-change-instruction-action.enum';
import { BattlePlayer } from './battle-player';
import { INativesStoreMapped } from 'src/app/share/models/natives-store-mapped.model';
import { BattleVisualObject } from './battle-visual-object';

export class BattleActor extends BattleVisualObject {
    id: number;
    externalId?: number;
    native: IActorNative;
    attackingSkill: BattleSkill;
    strength: number;
    willpower: number;
    constitution: number;
    speed: number;
    skills: BattleSkill[];
    actionPointsIncome: number;
    buffs: BattleBuff[];
    initiativePosition: number;
    health: number;
    owner?: BattlePlayer;
    isAlive: boolean;
    x: number;
    y: number;
    tempTile: BattleTile;
    z: number;
    maxHealth: number;
    actionPoints: number;
    skillPower: number;
    attackPower: number;
    initiative: number;
    armor: ITagSynergy[];
    attackModifiers: ITagSynergy[];

    synchronize(sync: ISyncActor, natives: INativesStoreMapped) {
        if (!this.skills) {
            this.skills = [];
        }
        if (!this.buffs) {
            this.buffs = [];
        }
        this.health = sync.health;
        if (!this.native || this.native.id !== sync.nativeId) {
            this.native = natives.actors.find(x => x.id === sync.nativeId);
        }
        if (this.attackingSkill) {
            this.attackingSkill.synchronize(sync.attackingSkill, natives);
        } else {
            this.attackingSkill = new BattleSkill(sync.attackingSkill, natives, this);
        }
        this.strength = sync.strength;
        this.willpower = sync.willpower;
        this.constitution = sync.constitution;
        this.speed = sync.speed;
        for (const syncSkill of sync.skills) {
            const skill = this.skills.find(x => x.id === syncSkill.id);
            if (skill) {
                skill.synchronize(syncSkill, natives);
            } else {
                this.skills.push(new BattleSkill(syncSkill, natives, this));
            }
        }
        for (let i = 0; i < this.skills.length; i++) {
            if (!sync.skills.find(x => x.id === this.skills[i].id)) {
                this.skills.splice(i, 1);
                i--;
            }
        }
        this.actionPointsIncome = sync.actionPointsIncome;
        for (const syncBuff of sync.buffs) {
            const buff = this.buffs.find(x => x.id === syncBuff.id);
            if (buff) {
                buff.synchronize(syncBuff, natives);
            } else {
                this.buffs.push(new BattleBuff(syncBuff, natives, this));
            }
        }
        for (let i = 0; i < this.buffs.length; i++) {
            if (!sync.buffs.find(x => x.id === this.buffs[i].id)) {
                this.buffs.splice(i, 1);
                i--;
            }
        }
        this.initiativePosition = sync.initiativePosition;
        if (!sync.ownerId && this.owner) {
            this.owner = null;
        } else if ((!this.owner && sync.ownerId) || (this.owner && this.owner.id !== sync.ownerId)) {
            this.owner = this.parent.players.find(x => x.id === sync.ownerId);
        }
        this.isAlive = sync.isAlive;
        if (!this.tempTile || this.x !== sync.x || this.y !== sync.y) {
            this.tempTile = this.parent.tiles.find(x => x.x === sync.x && x.y === sync.y);
        }
        this.x = sync.x;
        this.y = sync.y;
        this.z = sync.z;
        this.maxHealth = sync.maxHealth;
        this.actionPoints = sync.actionPoints;
        this.skillPower = sync.skillPower;
        this.attackPower = sync.attackPower;
        this.initiative = sync.initiative;
        this.armor = sync.armor;
        this.attackModifiers = sync.attackModifiers;
    }

    constructor(sync: ISyncActor, natives: INativesStoreMapped, public parent: BattleScene) {
        super();
        this.id = sync.id;
        this.externalId = sync.externalId;
        this.synchronize(sync, natives);
        this.tempTile.tempActor = this;
    }

    attack(targetX: number, targetY: number) {
        // TODO
    }

    move(targetX: number, targetY: number) {
        // TODO
    }

    cast(skill: BattleSkill, targetX: number, targetY: number) {
        // TODO
    }

    wait() {
        // TODO
    }

    orderAttack(targetX: number, targetY: number): boolean {
        this.parent.addEventBySignature({
            action: BattleChangeInstructionAction.Attack,
            actor: this,
            x: targetX,
            y: targetY
        });
        return true;
    }

    orderMove(targetX: number, targetY: number): boolean {
        this.parent.addEventBySignature({
            action: BattleChangeInstructionAction.Move,
            actor: this,
            x: targetX,
            y: targetY
        });
        return true;
    }

    orderCast(targetSkill: BattleSkill, targetX: number, targetY: number) {
        this.parent.addEventBySignature({
            action: BattleChangeInstructionAction.Attack,
            actor: this,
            x: targetX,
            y: targetY,
            skill: targetSkill
        });
        return true;
    }

    orderWait() {
        this.parent.addEventBySignature({
            action: BattleChangeInstructionAction.Attack,
            actor: this
        });
        return true;
    }

    update(milliseconds: number) {
        super.update(milliseconds);
    }
}
