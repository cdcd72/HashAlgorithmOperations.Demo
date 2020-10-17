using NUnit.Framework;

namespace HashCore.Test
{
    public class BaseHashToolTests
    {
        private BaseHashTool baseHashTool;

        [SetUp]
        public void Setup()
        {
            baseHashTool = new BaseHashTool();
        }

        #region HMAC - SHA256 Tests

        /// <summary>
        /// 產生 HMACSHA256 計算後雜湊值成功
        /// </summary>
        [Test]
        [Category("HMACSHA256")]
        public void GenerateHMACSHA256HashSuccess()
        {
            var message = "Test";
            var key = "kTvPDb2HY3AR4!S@yGy=eWWFDt6yQXByqxBkG8pHrEcabc8yN7$cE5V2@upC3Zs87vcfG3PpKedbyc#2qQS6eW!th6GRDpsnxus#acbsZ%zx##r2cs6C3KMS%#Nu?KKa";
            var hash = baseHashTool.HMACSHA256(message, key);
            Assert.IsTrue(hash.Length > 0);
        }

        /// <summary>
        /// 校驗 HMACSHA256 雜湊成功
        /// </summary>
        [Test]
        [Category("HMACSHA256")]
        public void VerifyHMACSHA256HashSuccess()
        {
            var message = "Test";
            var key = "kTvPDb2HY3AR4!S@yGy=eWWFDt6yQXByqxBkG8pHrEcabc8yN7$cE5V2@upC3Zs87vcfG3PpKedbyc#2qQS6eW!th6GRDpsnxus#acbsZ%zx##r2cs6C3KMS%#Nu?KKa";
            var hash = baseHashTool.HMACSHA256(message, key);
            Assert.IsTrue(baseHashTool.VerifyHMACSHA256(message, key, hash));
        }

        /// <summary>
        /// 校驗 HMACSHA256 雜湊失敗(訊息不一致)
        /// </summary>
        [Test]
        [Category("HMACSHA256")]
        public void VerifyHMACSHA256HashFail_MessageMissMatch()
        {
            var message = "Test";
            var anotherMessage = "Test1";
            var key = "kTvPDb2HY3AR4!S@yGy=eWWFDt6yQXByqxBkG8pHrEcabc8yN7$cE5V2@upC3Zs87vcfG3PpKedbyc#2qQS6eW!th6GRDpsnxus#acbsZ%zx##r2cs6C3KMS%#Nu?KKa";
            var hash = baseHashTool.HMACSHA256(message, key);
            Assert.IsFalse(baseHashTool.VerifyHMACSHA256(anotherMessage, key, hash));
        }

        /// <summary>
        /// 校驗 HMACSHA256 雜湊失敗(秘密金鑰不一致)
        /// </summary>
        [Test]
        [Category("HMACSHA256")]
        public void VerifyHMACSHA256HashFail_KeyMissMatch()
        {
            var message = "Test";
            var key = "kTvPDb2HY3AR4!S@yGy=eWWFDt6yQXByqxBkG8pHrEcabc8yN7$cE5V2@upC3Zs87vcfG3PpKedbyc#2qQS6eW!th6GRDpsnxus#acbsZ%zx##r2cs6C3KMS%#Nu?KKa";
            var anotherKey = "aTvPDb2HY3AR4!S@yGy=eWWFDt6yQXByqxBkG8pHrEcabc8yN7$cE5V2@upC3Zs87vcfG3PpKedbyc#2qQS6eW!th6GRDpsnxus#acbsZ%zx##r2cs6C3KMS%#Nu?KKa";
            var hash = baseHashTool.HMACSHA256(message, key);
            Assert.IsFalse(baseHashTool.VerifyHMACSHA256(message, anotherKey, hash));
        }

        #endregion
    }
}