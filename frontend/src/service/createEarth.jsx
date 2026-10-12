
import * as THREE from "three";

import dayMapImage from "../assets/8k_earth_daymap.jpg";
import nightMapImage from "../assets/8k_earth_nightmap.jpg";
import cloudMapImage from "../assets/8k_earth_clouds.jpg";

export function createEarth(scene) {
    const textureLoader = new THREE.TextureLoader();

    // TEXTURES
    const dayMap = textureLoader.load(dayMapImage);
    dayMap.colorSpace = THREE.SRGBColorSpace;

    const nightMap = textureLoader.load(nightMapImage);
    nightMap.colorSpace = THREE.SRGBColorSpace;

    const cloudMap = textureLoader.load(cloudMapImage);

    // EARTH GROUP
    const earthGroup = new THREE.Group();
    earthGroup.rotation.z = (-23.4 * Math.PI) / 180;
    scene.add(earthGroup);

    // EARTH SURFACE
    const geometry = new THREE.SphereGeometry(1, 64, 64);

    const earthMat = new THREE.MeshStandardMaterial({
        map: dayMap,
        roughness: 0.9,
        metalness: 0
    });

    const earthMesh = new THREE.Mesh(geometry, earthMat);
    earthGroup.add(earthMesh);

    // NIGHT LIGHTS
    const nightMat = new THREE.MeshBasicMaterial({
        map: nightMap,
        blending: THREE.AdditiveBlending,
        transparent: true,
        opacity: 0.8,
        depthWrite: false
    });

    const nightMesh = new THREE.Mesh(
        new THREE.SphereGeometry(1.002, 64, 64),
        nightMat
    );

    earthGroup.add(nightMesh);

    // CLOUDS
    const cloudGeometry = new THREE.SphereGeometry(1.01, 64, 64);

    const cloudMat = new THREE.MeshStandardMaterial({
        map: cloudMap,
        transparent: true,
        opacity: 0.35,
        depthWrite: false,
        side: THREE.FrontSide
    });

    const cloudMesh = new THREE.Mesh(cloudGeometry, cloudMat);
    earthGroup.add(cloudMesh);

    // LIGHTING
    const sunLight = new THREE.DirectionalLight(0xffffff, 3.5);
    sunLight.position.set(5, 2, 5);
    scene.add(sunLight);

    const ambientLight = new THREE.AmbientLight(0xffffff, 0.25);
    scene.add(ambientLight);

    // ANIMATION
    function update() {
        earthMesh.rotation.y += 0.001;
        nightMesh.rotation.y += 0.001;
        cloudMesh.rotation.y += 0.0012;
    }

    // CLEANUP
    function dispose() {
        scene.remove(earthGroup);
        scene.remove(sunLight);
        scene.remove(ambientLight);

        geometry.dispose();
        nightMesh.geometry.dispose();
        cloudGeometry.dispose();

        earthMat.dispose();
        nightMat.dispose();
        cloudMat.dispose();

        dayMap.dispose();
        nightMap.dispose();
        cloudMap.dispose();
    }

    return {
        earthMesh,
        earthGroup,
        update,
        dispose
    };
}
