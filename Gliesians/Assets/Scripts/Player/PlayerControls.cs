// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class PlayerControls : IInputActionCollection
{
    private InputActionAsset asset;
    public PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3961a350-e25d-4340-b35c-a8da55b9762b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""id"": ""e4a67271-dacb-46c8-9f4b-3ba0e06e5f3a"",
                    ""expectedControlLayout"": ""Vector2"",
                    ""continuous"": true,
                    ""passThrough"": false,
                    ""initialStateCheck"": true,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Look"",
                    ""id"": ""6c1620fe-d63a-4ca0-893d-b35260dcf32e"",
                    ""expectedControlLayout"": ""Vector2"",
                    ""continuous"": true,
                    ""passThrough"": true,
                    ""initialStateCheck"": true,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Jump"",
                    ""id"": ""4decb3f4-7cfc-4a8d-a03a-a7423bbfd6e7"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Jump2"",
                    ""id"": ""67a7ec4e-2a64-428a-bfdd-0a46fee1b288"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Time"",
                    ""id"": ""2a3c2fef-bfc8-4dc7-a1e5-559c995b517b"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Shot"",
                    ""id"": ""d66a7344-5b9a-489e-9e58-0f848f050745"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Aim"",
                    ""id"": ""d66a7344-5b9a-489e-9e58-0f848f050745"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Switch"",
                    ""id"": ""31d8505d-2ce5-48f4-833e-5bca3057b8a9"",
                    ""expectedControlLayout"": ""Vector2"",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Next"",
                    ""id"": ""52a37733-c005-4be4-9003-3453fb1972ef"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Previous"",
                    ""id"": ""52a37733-c005-4be4-9003-3453fb1972ef"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Reload"",
                    ""id"": ""52a37733-c005-4be4-9003-3453fb1972ef"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Ability1"",
                    ""id"": ""52a37733-c005-4be4-9003-3453fb1972ef"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Ability2"",
                    ""id"": ""52a37733-c005-4be4-9003-3453fb1972ef"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Ability3"",
                    ""id"": ""52a37733-c005-4be4-9003-3453fb1972ef"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Ability4"",
                    ""id"": ""52a37733-c005-4be4-9003-3453fb1972ef"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Action"",
                    ""id"": ""52a37733-c005-4be4-9003-3453fb1972ef"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                },
                {
                    ""name"": ""Inventory"",
                    ""id"": ""0bbefdac-6fa1-4d71-83b9-af904bf9fe14"",
                    ""expectedControlLayout"": """",
                    ""continuous"": false,
                    ""passThrough"": false,
                    ""initialStateCheck"": false,
                    ""processors"": """",
                    ""interactions"": """",
                    ""bindings"": []
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""arrows "",
                    ""id"": ""fb3f4d3f-86b0-4de1-aec5-d5d0b4aa9bef"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""up"",
                    ""id"": ""698843b0-10c1-4c24-9eef-dacdecc40d7c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cf9ffa51-f4be-4da4-a327-aaad30da21de"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""left"",
                    ""id"": ""cd29497f-cf90-4ece-95ca-a0498132c460"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""128f0d13-be46-4ce3-bae5-a53ba400ad6f"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""zqsd"",
                    ""id"": ""32fd5797-f534-446a-af45-3ea45ff97d33"",
                    ""path"": ""Dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f034abd2-b804-4dae-a268-5fc8ca0b539b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dc25f2e4-1a2e-4989-8a84-38d2252d491b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fe6fb270-84df-40d4-8f75-ec37776b999b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8202bba9-35eb-4f38-8aaa-74b309c04a56"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""351d8b68-c2d1-4596-9a90-8307d3f077dc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""393864cd-d6d1-46b1-b473-f43ac9dbdd22"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""9d23da1e-722c-4acb-bba7-4aee629d5111"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.05,y=0.05)"",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f7b59cfd-1986-4cfb-b4cd-6c2aa3167f25"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""up"",
                    ""id"": ""abe5beb0-d26b-4e71-a657-f13ea9ff9270"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(max=1)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""down"",
                    ""id"": ""45e43b68-6920-4547-8700-065c0331ee58"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-1)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bf995c6a-9eee-4e65-a791-36e1c5555ecc"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(min=-1)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7da05630-5cba-4069-88d2-d3c13958e24a"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(max=1)"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""bf79275f-770e-4fa5-ba54-03fd13e515aa"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""58f8c4bf-b204-4844-ac39-13ca61a4a200"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""bf1d96c4-87dc-49a3-8e26-8fe7c1912e1b"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Time"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0dc6902a-8854-46b3-8d0a-870983a260ce"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Time"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9cad8740-d27a-4bc1-b753-55a462fad509"",
                    ""path"": ""<DualShockGamepad>/leftStickPress"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Jump2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c050ff24-147c-4417-9f6f-27b5b43b5616"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Jump2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""09668777-b1d7-4823-917e-c65335c6915f"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Shot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fb325b96-7259-45d3-a064-a7515d931a99"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Shot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c76afd20-9789-444a-8cc5-2d8d041487b2"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3b0c363c-52c6-41e3-820a-556864a207f8"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cdaeacbe-48ac-4a99-9da9-9a4e77fdc28c"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""21aa4324-563b-4125-9c50-9dfadbca12b8"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Previous"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9336a06c-5a94-420c-97da-cf451a1d2978"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""c5c9413b-ac03-437c-b49a-1b179bbdc39d"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""874208c9-56f4-414e-8c65-3ddda3f6881c"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2232f8f5-8429-4c9a-a8dc-070553cb4559"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Ability1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""68853514-22db-48e3-abb9-e3f5454ca020"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b273c86e-5d02-489f-9974-7a176c70a119"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Ability2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""576f7da1-f4a9-4008-acf6-4984ba43242c"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""89b61c7b-1a7e-481c-b8db-d799de2268bb"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""cdefcd2a-ef7b-43b0-a6f5-0f6f8dba9db2"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""5fac9541-5f59-48a1-ad95-a9653701c7d9"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1fd34a2f-3f98-45af-93d9-008759d1e06d"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Ability3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6d9777a0-48da-4730-93ef-d41ebe4a6447"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Ability3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""38345de3-28bb-4207-8f15-9afef6812704"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""58696b38-db64-4ac1-ad23-6b5f91fb6674"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Ability4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                },
                {
                    ""name"": """",
                    ""id"": ""9c185d55-8098-4c6a-9e4b-8e727f48b6d9"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard and Mouse"",
                    ""action"": ""Ability4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false,
                    ""modifiers"": """"
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""basedOn"": """",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""basedOn"": """",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_Movement = m_Player.GetAction("Movement");
        m_Player_Look = m_Player.GetAction("Look");
        m_Player_Jump = m_Player.GetAction("Jump");
        m_Player_Jump2 = m_Player.GetAction("Jump2");
        m_Player_Time = m_Player.GetAction("Time");
        m_Player_Shot = m_Player.GetAction("Shot");
        m_Player_Aim = m_Player.GetAction("Aim");
        m_Player_Switch = m_Player.GetAction("Switch");
        m_Player_Next = m_Player.GetAction("Next");
        m_Player_Previous = m_Player.GetAction("Previous");
        m_Player_Reload = m_Player.GetAction("Reload");
        m_Player_Ability1 = m_Player.GetAction("Ability1");
        m_Player_Ability2 = m_Player.GetAction("Ability2");
        m_Player_Ability3 = m_Player.GetAction("Ability3");
        m_Player_Ability4 = m_Player.GetAction("Ability4");
        m_Player_Action = m_Player.GetAction("Action");
        m_Player_Inventory = m_Player.GetAction("Inventory");
    }

    ~PlayerControls()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes
    {
        get => asset.controlSchemes;
    }

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private InputAction m_Player_Movement;
    private InputAction m_Player_Look;
    private InputAction m_Player_Jump;
    private InputAction m_Player_Jump2;
    private InputAction m_Player_Time;
    private InputAction m_Player_Shot;
    private InputAction m_Player_Aim;
    private InputAction m_Player_Switch;
    private InputAction m_Player_Next;
    private InputAction m_Player_Previous;
    private InputAction m_Player_Reload;
    private InputAction m_Player_Ability1;
    private InputAction m_Player_Ability2;
    private InputAction m_Player_Ability3;
    private InputAction m_Player_Ability4;
    private InputAction m_Player_Action;
    private InputAction m_Player_Inventory;
    public struct PlayerActions
    {
        private PlayerControls m_Wrapper;
        public PlayerActions(PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement { get { return m_Wrapper.m_Player_Movement; } }
        public InputAction @Look { get { return m_Wrapper.m_Player_Look; } }
        public InputAction @Jump { get { return m_Wrapper.m_Player_Jump; } }
        public InputAction @Jump2 { get { return m_Wrapper.m_Player_Jump2; } }
        public InputAction @Time { get { return m_Wrapper.m_Player_Time; } }
        public InputAction @Shot { get { return m_Wrapper.m_Player_Shot; } }
        public InputAction @Aim { get { return m_Wrapper.m_Player_Aim; } }
        public InputAction @Switch { get { return m_Wrapper.m_Player_Switch; } }
        public InputAction @Next { get { return m_Wrapper.m_Player_Next; } }
        public InputAction @Previous { get { return m_Wrapper.m_Player_Previous; } }
        public InputAction @Reload { get { return m_Wrapper.m_Player_Reload; } }
        public InputAction @Ability1 { get { return m_Wrapper.m_Player_Ability1; } }
        public InputAction @Ability2 { get { return m_Wrapper.m_Player_Ability2; } }
        public InputAction @Ability3 { get { return m_Wrapper.m_Player_Ability3; } }
        public InputAction @Ability4 { get { return m_Wrapper.m_Player_Ability4; } }
        public InputAction @Action { get { return m_Wrapper.m_Player_Action; } }
        public InputAction @Inventory { get { return m_Wrapper.m_Player_Inventory; } }
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled { get { return Get().enabled; } }
        public InputActionMap Clone() { return Get().Clone(); }
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                Jump2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump2;
                Jump2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump2;
                Jump2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump2;
                Time.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTime;
                Time.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTime;
                Time.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTime;
                Shot.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShot;
                Shot.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShot;
                Shot.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShot;
                Aim.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                Aim.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                Aim.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAim;
                Switch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                Switch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                Switch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitch;
                Next.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNext;
                Next.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNext;
                Next.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnNext;
                Previous.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrevious;
                Previous.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrevious;
                Previous.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPrevious;
                Reload.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                Reload.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                Reload.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnReload;
                Ability1.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility1;
                Ability1.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility1;
                Ability1.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility1;
                Ability2.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility2;
                Ability2.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility2;
                Ability2.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility2;
                Ability3.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility3;
                Ability3.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility3;
                Ability3.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility3;
                Ability4.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility4;
                Ability4.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility4;
                Ability4.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAbility4;
                Action.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                Action.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                Action.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAction;
                Inventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                Inventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
                Inventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnInventory;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.canceled += instance.OnMovement;
                Look.started += instance.OnLook;
                Look.performed += instance.OnLook;
                Look.canceled += instance.OnLook;
                Jump.started += instance.OnJump;
                Jump.performed += instance.OnJump;
                Jump.canceled += instance.OnJump;
                Jump2.started += instance.OnJump2;
                Jump2.performed += instance.OnJump2;
                Jump2.canceled += instance.OnJump2;
                Time.started += instance.OnTime;
                Time.performed += instance.OnTime;
                Time.canceled += instance.OnTime;
                Shot.started += instance.OnShot;
                Shot.performed += instance.OnShot;
                Shot.canceled += instance.OnShot;
                Aim.started += instance.OnAim;
                Aim.performed += instance.OnAim;
                Aim.canceled += instance.OnAim;
                Switch.started += instance.OnSwitch;
                Switch.performed += instance.OnSwitch;
                Switch.canceled += instance.OnSwitch;
                Next.started += instance.OnNext;
                Next.performed += instance.OnNext;
                Next.canceled += instance.OnNext;
                Previous.started += instance.OnPrevious;
                Previous.performed += instance.OnPrevious;
                Previous.canceled += instance.OnPrevious;
                Reload.started += instance.OnReload;
                Reload.performed += instance.OnReload;
                Reload.canceled += instance.OnReload;
                Ability1.started += instance.OnAbility1;
                Ability1.performed += instance.OnAbility1;
                Ability1.canceled += instance.OnAbility1;
                Ability2.started += instance.OnAbility2;
                Ability2.performed += instance.OnAbility2;
                Ability2.canceled += instance.OnAbility2;
                Ability3.started += instance.OnAbility3;
                Ability3.performed += instance.OnAbility3;
                Ability3.canceled += instance.OnAbility3;
                Ability4.started += instance.OnAbility4;
                Ability4.performed += instance.OnAbility4;
                Ability4.canceled += instance.OnAbility4;
                Action.started += instance.OnAction;
                Action.performed += instance.OnAction;
                Action.canceled += instance.OnAction;
                Inventory.started += instance.OnInventory;
                Inventory.performed += instance.OnInventory;
                Inventory.canceled += instance.OnInventory;
            }
        }
    }
    public PlayerActions @Player
    {
        get
        {
            return new PlayerActions(this);
        }
    }
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.GetControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.GetControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnJump2(InputAction.CallbackContext context);
        void OnTime(InputAction.CallbackContext context);
        void OnShot(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnSwitch(InputAction.CallbackContext context);
        void OnNext(InputAction.CallbackContext context);
        void OnPrevious(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnAbility1(InputAction.CallbackContext context);
        void OnAbility2(InputAction.CallbackContext context);
        void OnAbility3(InputAction.CallbackContext context);
        void OnAbility4(InputAction.CallbackContext context);
        void OnAction(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
    }
}
