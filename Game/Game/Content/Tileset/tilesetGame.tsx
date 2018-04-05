<?xml version="1.0" encoding="UTF-8"?>
<tileset name="tilesetGame" tilewidth="32" tileheight="32" tilecount="30" columns="5">
 <image source="../Textures/tileseGamet.png" width="160" height="192"/>
 <terraintypes>
  <terrain name="Dirt" tile="6"/>
  <terrain name="Stone" tile="25"/>
 </terraintypes>
 <tile id="0" terrain=",,,0"/>
 <tile id="1" terrain=",,0,0"/>
 <tile id="2" terrain=",,0,"/>
 <tile id="3">
  <properties>
   <property name="nez:isSlope" type="bool" value="true"/>
   <property name="nez:slopeTopLeft" type="int" value="32"/>
   <property name="nez:slopeTopRight" type="int" value="0"/>
  </properties>
  <objectgroup draworder="index">
   <object id="2" x="32" y="0">
    <polygon points="0,0 -32,32 0,32"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="4">
  <properties>
   <property name="nez:isSlope" type="bool" value="true"/>
   <property name="nez:slopeTopLeft" type="int" value="0"/>
   <property name="nez:slopeTopRight" type="int" value="32"/>
  </properties>
  <objectgroup draworder="index">
   <object id="1" x="0" y="0">
    <polygon points="0,0 32,32 0,32"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="5" terrain=",0,,0"/>
 <tile id="6" terrain="0,0,0,0"/>
 <tile id="7" terrain="0,,0,"/>
 <tile id="10" terrain=",0,,"/>
 <tile id="11" terrain="0,0,,"/>
 <tile id="12" terrain="0,,,"/>
 <tile id="16" terrain="0,0,0,"/>
 <tile id="17" terrain="0,0,,0"/>
 <tile id="18">
  <properties>
   <property name="nez:isSlope" type="bool" value="true"/>
   <property name="nez:slopeTopLeft" type="int" value="0"/>
   <property name="nez:slopeTopRight" type="int" value="0"/>
  </properties>
  <objectgroup draworder="index">
   <object id="1" x="0.125" y="0.125">
    <polygon points="0,0 31.875,31.875 31.875,-0.125"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="19">
  <properties>
   <property name="nez:isSlope" type="bool" value="true"/>
   <property name="nez:slopeTopLeft" type="int" value="0"/>
   <property name="nez:slopeTopRight" type="int" value="0"/>
  </properties>
  <objectgroup draworder="index">
   <object id="1" x="32" y="0">
    <polygon points="0,0 -32,32 -32,0"/>
   </object>
  </objectgroup>
 </tile>
 <tile id="20">
  <properties>
   <property name="nez:isOneWayPlatform" type="bool" value="true"/>
  </properties>
 </tile>
 <tile id="21" terrain="0,,0,0"/>
 <tile id="22" terrain=",0,0,0"/>
 <tile id="25" terrain="1,1,1,1"/>
 <tile id="26" terrain="1,1,1,1"/>
 <wangsets>
  <wangset name="Ground" tile="-1"/>
 </wangsets>
</tileset>
