using UnityEngine;
using Zenject;

namespace Entities.Enemy.Attacks
{
    public class InstantiateBulletProvider : MonoBehaviour
    {
        [Inject] private DiContainer _container;

        public void Instantiate(Bullet bullet, Transform shootPoint, Player.Player target)
        {
            _container.InstantiatePrefab(bullet, shootPoint.position, target.gameObject.transform.rotation, shootPoint);
        }
    
        public void Instantiate(Bullet bullet, Transform shootPoint)
        {
            _container.InstantiatePrefab(bullet, shootPoint.position, Quaternion.identity, shootPoint);
        }
    }
}
