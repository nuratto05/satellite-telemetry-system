import { useEffect, useRef } from "react";
import * as THREE from "three";
import { OrbitControls } from "three/addons/controls/OrbitControls.js";

import { createEarth } from "../service/createEarth"; 

function Earth() {
    const containerRef = useRef(null);
    // SCENE SETUP (runs once)
    useEffect(() => {
        // SCENE
        const scene = new THREE.Scene();

        // CAMERA
        const camera = new THREE.PerspectiveCamera(75, window.innerWidth / window.innerHeight, 0.1, 1000);
        camera.position.z = 5;

        // RENDERER
        const renderer = new THREE.WebGLRenderer({ antialias: true });
        renderer.setSize(window.innerWidth, window.innerHeight);
        renderer.setPixelRatio(window.devicePixelRatio);
        renderer.outputColorSpace = THREE.SRGBColorSpace;
        renderer.toneMapping = THREE.ACESFilmicToneMapping;
        renderer.toneMappingExposure = 1.1;
        containerRef.current.appendChild(renderer.domElement);

        // ORBIT CONTROLS
        const controls = new OrbitControls(camera, renderer.domElement);
        controls.enableDamping = true;
        controls.minDistance = 0.2;

        // EARTH
        const earth = createEarth(scene);

        // ANIMATION
        let animationId;

        function animate() {
            animationId = requestAnimationFrame(animate);

            earth.update();

            controls.update();

            renderer.render(scene, camera);
        }

        animate();

        return () => {
            cancelAnimationFrame(animationId);

            controls.dispose();
            renderer.dispose();

            earth.dispose();

            renderer.domElement.remove();
        };
    }, []);


    return (
        <div ref={containerRef} style={{ width: "100vw", height: "100vh", position: "absolute" }} />
    );
}

export default Earth;