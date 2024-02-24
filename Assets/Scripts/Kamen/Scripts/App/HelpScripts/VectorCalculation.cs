using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kamen.Calculation
{
    public static class VectorCalculation
    {
        #region Classes

        private struct ResultsInfo
        {
            #region ResultsInfo Properties

            public int AmountCorrectResults { get; private set; }
            public int AmountIncorrectResults { get; private set; }
            public bool IsBeyondTheMax { get; private set; }

            #endregion

            #region ResultsInfo Methods

            public void IncreaseCorrectResults() => AmountCorrectResults++;
            public void DecreaseCorrectResults()
            {
                if (AmountCorrectResults > 0) AmountIncorrectResults--;
            }

            public void IncreaseIncorrectResults() => AmountIncorrectResults++;
            public void DecreaseIncorrectResults()
            {
                if (AmountIncorrectResults > 0) AmountCorrectResults--;
            }

            public void ActivateGoingBeyond() => IsBeyondTheMax = true;

            #endregion
        }

        #endregion

        #region Distance Methods

        public static bool CheckDistance(Vector2 firstVector, Vector2 secondVector, float linearDistance, bool isUseMaxDistance, float maxLinearDistance = float.MaxValue, int amountRightResult = 2)
        {
            ResultsInfo resultsInfo = new();

            GetLinearDistanceResult(firstVector.x, secondVector.x, linearDistance, ref resultsInfo, isUseMaxDistance ? maxLinearDistance : -1);
            GetLinearDistanceResult(firstVector.y, secondVector.y, linearDistance, ref resultsInfo, isUseMaxDistance ? maxLinearDistance : -1);

            return CalculateResult(ref resultsInfo, amountRightResult);
        }
        public static bool CheckDistance(Vector2Int firstVector, Vector2Int secondVector, int linearDistance, bool isUseMaxDistance, int maxLinearDistance = int.MaxValue, int amountRightResult = 2)
        {
            ResultsInfo resultsInfo = new();

            GetLinearDistanceResult(firstVector.x, secondVector.x, linearDistance, ref resultsInfo, isUseMaxDistance ? maxLinearDistance : -1);
            GetLinearDistanceResult(firstVector.y, secondVector.y, linearDistance, ref resultsInfo, isUseMaxDistance ? maxLinearDistance : -1);

            return CalculateResult(ref resultsInfo, amountRightResult);
        }
        public static bool CheckDistance(Vector3 firstVector, Vector3 secondVector, float linearDistance, bool isUseMaxDistance, float maxLinearDistance = float.MaxValue, int amountRightResult = 3)
        {
            ResultsInfo resultsInfo = new();

            GetLinearDistanceResult(firstVector.x, secondVector.x, linearDistance, ref resultsInfo, isUseMaxDistance ? maxLinearDistance : -1);
            GetLinearDistanceResult(firstVector.y, secondVector.y, linearDistance, ref resultsInfo, isUseMaxDistance ? maxLinearDistance : -1);
            GetLinearDistanceResult(firstVector.z, secondVector.z, linearDistance, ref resultsInfo, isUseMaxDistance ? maxLinearDistance : -1);

            return CalculateResult(ref resultsInfo, amountRightResult);
        }
        public static bool CheckDistance(Vector3Int firstVector, Vector3Int secondVector, int linearDistance, bool isUseMaxDistance, int maxLinearDistance = int.MaxValue, int amountRightResult = 2)
        {
            ResultsInfo resultsInfo = new();

            GetLinearDistanceResult(firstVector.x, secondVector.x, linearDistance, ref resultsInfo, isUseMaxDistance ? maxLinearDistance : -1);
            GetLinearDistanceResult(firstVector.y, secondVector.y, linearDistance, ref resultsInfo, isUseMaxDistance ? maxLinearDistance : -1);
            GetLinearDistanceResult(firstVector.z, secondVector.z, linearDistance, ref resultsInfo, isUseMaxDistance ? maxLinearDistance : -1);

            return CalculateResult(ref resultsInfo, amountRightResult);
        }

        #endregion

        #region Distance Calculation Methods

        private static void GetLinearDistanceResult(float first, float second, float distance, ref ResultsInfo resultsInfo, float maxDistance = -1)
        {
            float foundDistance = Mathf.Abs(first - second);
            if (foundDistance == distance) resultsInfo.IncreaseCorrectResults();
            else resultsInfo.IncreaseIncorrectResults();

            if (maxDistance != -1 && foundDistance > maxDistance) resultsInfo.ActivateGoingBeyond();
        }
        private static bool CalculateResult(ref ResultsInfo resultsInfo, int rightAmount)
        {
            if (resultsInfo.AmountCorrectResults == rightAmount && !resultsInfo.IsBeyondTheMax) return true;
            else return false;
        }

        #endregion

        #region Compare Methods

        public static CompareResults CompareVectors(Vector3 firstVector, Vector3 secondVector, Vector3 sign)
        {
            List<CompareResults> results = new List<CompareResults>
            {
                CompareTwoValue(sign.x * firstVector.x, sign.x * secondVector.x),
                CompareTwoValue(sign.y * firstVector.y, sign.y * secondVector.y),
                CompareTwoValue(sign.z * firstVector.z, sign.z * secondVector.z)
            };

            return CalculateRealResult(results);
        }
        public static CompareResults CompereWhatMore(Vector3 firstVector, Vector3 secondVector, Vector3 usedAxis)
        {
            CompareResults result = CompareResults.Not;

            if (usedAxis.x != 0) result = CompareTwoValue(firstVector.x, secondVector.x);
            if (usedAxis.y != 0) result = CompareTwoValue(firstVector.y, secondVector.y);
            if (usedAxis.z != 0) result = CompareTwoValue(firstVector.z, secondVector.z);

            return result;
        }

        #endregion

        #region Compare Calculation Methods

        private static CompareResults CompareTwoValue(float firstValeu, float secondValue)
        {
            if (firstValeu > secondValue) return CompareResults.More;
            else if (firstValeu < secondValue) return CompareResults.Less;
            else return CompareResults.Equals;
        }
        private static CompareResults CalculateRealResult(List<CompareResults> results)
        {
            if (results.Contains(CompareResults.More) && results.Contains(CompareResults.Less)) return CompareResults.Not;
            else if (results.Contains(CompareResults.More)) return CompareResults.More;
            else if (results.Contains(CompareResults.Less)) return CompareResults.Less;
            else return CompareResults.Equals;
        }

        #endregion

        #region Point Methods

        public static Vector2 GetMiddlePoint(Vector2 startPosition, Vector2 endPosition) => (startPosition + endPosition) / 2.0f;
        public static Vector2Int GetMiddlePoint(Vector2Int startPosition, Vector2Int endPosition) => (startPosition + endPosition) / 2;
        public static Vector3 GetMiddlePoint(Vector3 startPosition, Vector3 endPosition) => (startPosition + endPosition) / 2.0f;
        public static Vector3Int GetMiddlePoint(Vector3Int startPosition, Vector3Int endPosition) => (startPosition + endPosition) / 2;

        #endregion
    }
}

