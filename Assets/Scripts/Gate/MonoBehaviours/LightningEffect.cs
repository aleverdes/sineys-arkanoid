using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TaigaGames.SineysArkanoid.Gate.MonoBehaviours
{
    public class LightningEffect : MonoBehaviour
    {
        [SerializeField] private Transform[] _path;
        [Min(1f), SerializeField] private float _frequency = 3f;
        [SerializeField] private float _maxAmplitude = 1f;
        [SerializeField] private Vector2 _segmentLength = new Vector2(0.5f, 7f);
        [SerializeField] private LineRenderer _lineRenderer;

        private float _updateTimer;
        private readonly List<Vector3> _points = new List<Vector3>();
        
        private void Update()
        {
            _updateTimer += Time.deltaTime;
            if (_updateTimer >= 1f / _frequency)
            {
                _updateTimer = 0f;
                UpdateLine();
            }
        }

        private void UpdateLine()
        {
            _points.Clear();
            _points.Add(_path[0].position);
            var lastPoint = _path[0].position;
            for (var i = 1; i < _path.Length; i++)
            {
                var angle = Vector2.SignedAngle(Vector2.right, _path[i].position - _path[i - 1].position);
                var totalDistance = Vector3.Distance(_path[i].position, _path[i - 1].position);
                while (totalDistance > 0f)
                {
                    var segmentLength = Mathf.Lerp(_segmentLength.x, _segmentLength.y, Random.value);
                    if (segmentLength > totalDistance) 
                        segmentLength = totalDistance;
                        
                    var amplitude = Mathf.Lerp(-_maxAmplitude, _maxAmplitude, Random.value);
                    var nextPointLocal = Vector3.right * segmentLength + Vector3.up * amplitude;
                    var nextPointGlobal = lastPoint + new Vector3(
                                              nextPointLocal.x * Mathf.Cos(angle * Mathf.Deg2Rad) - nextPointLocal.y * Mathf.Sin(angle * Mathf.Deg2Rad), 
                                              nextPointLocal.x * Mathf.Sin(angle * Mathf.Deg2Rad) + nextPointLocal.y * Mathf.Cos(angle * Mathf.Deg2Rad), 
                                              0f);
                    _points.Add(nextPointGlobal);
                    totalDistance -= segmentLength;
                    lastPoint = nextPointGlobal;
                }
            }

            var points = _points.ToArray();
            points[^1] = _path[^1].position;
            _lineRenderer.positionCount = points.Length;
            _lineRenderer.SetPositions(points);
        }
    }
}