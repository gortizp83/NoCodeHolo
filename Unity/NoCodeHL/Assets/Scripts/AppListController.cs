using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.Collections;
using Newtonsoft.Json;
using NoCodeHL.JsonModels;

namespace NoCodeHL
{
    public class AppListController : MonoBehaviour
    {
        /// <summary>
        /// The Object Collection used to show the list of apps available to the user.
        /// </summary>
        [SerializeField]
        public ObjectCollection _objectCollection;

        [SerializeField]
        public GameObject _buttonPrefab;

        private string _listOfAppsMock = @"[
                {
                    'blobStorageUri': 'https://nocodehl.blob.core.windows.net/applist-static/app1.json',
                    'displayName': 'First app',
                    'description': 'First app generated using manual json file typing' },
                {
                    'blobStorageUri': 'https://nocodehl.blob.core.windows.net/applist-static/app2.json',
                    'displayName': 'Hello Holo!',
                    'description': 'Hello world test app' 
                },
                {
                    'blobStorageUri': 'https://nocodehl.blob.core.windows.net/applist-static/app3.json',
                    'displayName': 'Demo Hackathon 2018',
                    'description': 'App for Demo day Hackathon 2018' 
                },
           ]";

        // Use this for initialization
        void Start()
        {
            if (_objectCollection)
            {
                foreach(var appDescription in GetAppList(_listOfAppsMock))
                {
                    var button = Instantiate(_buttonPrefab, _objectCollection.transform);
                    var textMesh = button.GetComponentInChildren<TextMesh>();
                    if (textMesh)
                    {
                        textMesh.text = appDescription.DisplayName;
                    }

                    var node = new CollectionNode();
                    node.transform = button.transform;
                    _objectCollection.NodeList.Add(node);
                    _objectCollection.Rows++;
                }

                _objectCollection.UpdateCollection();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerable<AppDescription> GetAppList(string appListString)
        {
            return JsonConvert.DeserializeObject<List<AppDescription>>(appListString);
        }
    }
}
