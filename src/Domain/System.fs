module Domain.System

type CharacterName = CharacterName of string
let characterName characterName =
    let toString (CharacterName name) = name
    toString characterName

[<AutoOpen>]
module Aspects =
    type AspectName = AspectName of string
    let aspectName aspectName =
        let toString (AspectName name) = name
        toString aspectName

    type Aspect =
    | HighConcept of AspectName
    | Trouble of AspectName
    | Other of AspectName

    let private nextAspect =
        function
        | 1 -> HighConcept (AspectName "")
        | 2 -> Trouble (AspectName "")
        | _ -> Other (AspectName "")

    let internal createAspects count =
        match count with
        | 0 -> []
        | negative when negative < 0 -> []
        | _ -> [ 1 .. count ]
            |> List.map nextAspect

[<AutoOpen>]
module Ladder =
    type Rank =
    | Legendary
    | Epic
    | Fantastic
    | Superb
    | Great
    | Good
    | Fair
    | Average
    | Mediocre
    | Poor
    | Terrible

    let rankValue =
        function
        | Legendary -> 8
        | Epic -> 7
        | Fantastic -> 6
        | Superb -> 5
        | Great -> 4
        | Good -> 3
        | Fair -> 2
        | Average -> 1
        | Mediocre -> 0
        | Poor -> -1
        | Terrible -> -2

[<AutoOpen>]
module Stress =
    type Boxes = Boxes of int
    let boxValue boxes =
        let toInt (Boxes value) = value
        toInt boxes

    type ConsequenceName = ConsequenceName of string
    let consequenceName consequenceName =
        let toString (ConsequenceName name) = name
        toString consequenceName

    type StressType =
    | Physical
    | Mental
    | General
    | NA

    type StressBox = {
        Type: StressType
        Usable: bool
        Filled: bool
        Stress: Boxes
    }

    type ConsequenceType =
    | Mild
    | Moderate
    | Severe

    type Consequence = {
        Type: ConsequenceType
        StressType: StressType
        Name: ConsequenceName
        Stress: Boxes
        Available: bool
    }

    // TODO: possibly rename/remove later on
    let toString consequence =
        let name = consequenceName consequence.Name
        let value = boxValue consequence.Stress
        sprintf "%s (%d)" name value

[<AutoOpen>]
module Stunts =
    type Refresh = Refresh of int

    type StuntName = StuntName of string
    let stuntName stuntName =
        let toString (StuntName name) = name
        toString stuntName

    type StuntActivation =
    | FatePoints of int
    | Scene
    | Conflict
    | Day
    | Session