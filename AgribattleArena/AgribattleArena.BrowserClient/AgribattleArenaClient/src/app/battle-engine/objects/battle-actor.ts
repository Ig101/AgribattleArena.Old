import { ITagSynergy } from 'src/app/share/models/tag-synergy.model';
import { BattleSkill, BattleBuff } from '.';
import { IActorNative } from 'src/app/share/models/natives';
import { BattleScene } from '../battle-scene';
import { ISyncActor } from 'src/app/share/models/synchronization';
import { BattleTile } from './battle-tile';
import { INativesStore } from 'src/app/share/models/natives-store.model';
import { BattleChangeInstructionAction } from 'src/app/share/models/enums/battle-change-instruction-action.enum';
import { BattlePlayer } from './battle-player';

export class BattleActor {
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

    synchronize(sync: ISyncActor, natives: INativesStore) {
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
        for (const i in sync.skills) {
            if (sync.skills.hasOwnProperty(i)) {
                const skill = this.skills.find(x => x.id === sync.skills[i].id);
                if (skill) {
                    skill.synchronize(sync.skills[i], natives);
                } else {
                    this.skills.push(new BattleSkill(sync.skills[i], natives, this));
                }
            }
        }
        for (const i in this.skills) {
            if (this.skills.hasOwnProperty(i) && !sync.skills.find(x => x.id === this.skills[i].id)) {
                this.skills[i] = null;
            }
        }
        this.actionPointsIncome = sync.actionPointsIncome;
        for (const i in sync.buffs) {
            if (sync.buffs.hasOwnProperty(i)) {
                const buff = this.buffs.find(x => x.id === sync.buffs[i].id);
                if (buff) {
                    buff.synchronize(sync.buffs[i], natives);
                } else {
                    this.buffs.push(new BattleBuff(sync.buffs[i], natives, this));
                }
            }
        }
        for (const i in this.buffs) {
            if (this.buffs.hasOwnProperty(i) && !sync.buffs.find(x => x.id === this.buffs[i].id)) {
                this.buffs[i] = null;
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

    constructor(sync: ISyncActor, natives: INativesStore, public parent: BattleScene) {
        this.id = sync.id;
        this.externalId = sync.externalId;
        this.synchronize(sync, natives);
    }

    async attack(targetX: number, targetY: number) {
        // TODO
    }

    async move(targetX: number, targetY: number) {
        // TODO
    }

    async cast(skill: BattleSkill, targetX: number, targetY: number) {
        // TODO
    }

    async wait() {
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
}
